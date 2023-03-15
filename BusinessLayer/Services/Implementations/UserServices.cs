using AutoMapper;
using BusinessLayer.DTOs.TagDtos;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer.Services.Implementations
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ITagRepository tagRepository;

        public UserServices(IUserRepository userRepository, IMapper mapper, ITagRepository tagRepository)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.tagRepository = tagRepository;
        }

        public async Task<bool> RegisterUserAsync(UserRegistrationRequestDto userRegistration)
        {
            if (userRepository.CheckIfEmailExists(userRegistration.Email))
            {
                throw new BadRequestException($"this email {userRegistration.Email} is already exist");
            }

            CreatePasswordHash(userRegistration.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User()
            {
                Username = userRegistration.Username,
                Email = userRegistration.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreationDate = DateTime.Now,
                VerificationToken = CreateRandomToken()
            };
            SendEmailWithVerificationToken(user.Email, user.VerificationToken);
            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> LoginUserAsync(UserLoginRequestDto userLogin)
        {
            var user = await userRepository.GetUserByEmail(userLogin.Email);
            if (user == null)
            {
                throw new BadRequestException($"this email {userLogin.Email} is not exist");
            }

            if (!VerifyPassword(userLogin.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new BadRequestException($"Password is not correct");
            }

            if (user.VerifiedDate == null)
            {
                throw new BadRequestException($"You must Verify your email");
            }
            return true;
        }

        public async Task<bool> VerifyEmailAsync(string token)
        {
            var user = userRepository.GetUserByVerifyToken(token);
            if (user == null)
            {
                throw new BadRequestException("Verify token is not valid");
            }
            user.VerifiedDate = DateTime.Now;
            await userRepository.SaveChangesAsync();

            return true;
        }

        private static void SendEmailWithVerificationToken(string email, string token)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("answer flow", "answerflowverification@outlook.com"));
            message.To.Add(new MailboxAddress("user", email));
            message.Subject = "Verification Account";
            message.Body = new TextPart("plain")
            {
                Text = $"this is the verification token : {token} , copy it and send it"

            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp-mail.outlook.com", 587, false);
                client.Authenticate("answerflowverification@outlook.com", "AbuSafat123456789");
                client.Send(message);
                client.Disconnect(true);
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            }
        }

        private static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var CommingPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return CommingPasswordHash.SequenceEqual(passwordHash);
            }
        }

        private static string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        public async Task FollowUserAsync(int userId, int followedUserId)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new BadRequestException($"there is no user with this id: {userId}");
            }
            var followedUser = await userRepository.GetUserById(followedUserId);
            if (followedUser == null)
            {
                throw new BadRequestException($"there is no user with this id: {followedUserId}");
            }
            user.FollowingUsers.Add(followedUser);
            await userRepository.SaveChangesAsync();
        }

        public async Task UnfollowUserAsync(int userId, int followedUserId)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new BadRequestException($"there is no user with this id: {userId}");
            }
            var followedUser = await userRepository.GetUserById(followedUserId);
            if (followedUser == null)
            {
                throw new BadRequestException($"there is no user with this id: {followedUserId}");
            }
            if (!user.FollowingUsers.Contains(followedUser))
            {
                throw new BadRequestException($"The user did't follow this user");
            }
            user.FollowingUsers.Remove(followedUser);
            await userRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserOverviewResponseDto>> GetFollowingUsersForUserByIdAsync(int userId)
        {
            var users = await userRepository.GetFollowingUsersForUserById(userId);
            return mapper.Map<IEnumerable<UserOverviewResponseDto>>(users);
        }

        public async Task FollowTagAsync(int userId, int tagId)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new BadRequestException($"there is no user with this id: {userId}");
            }
            var tag = await tagRepository.GetTagByIdAsync(tagId);
            if (tag == null)
            {
                throw new BadRequestException($"there is no tag with this id: {tagId}");
            }
            user.Tags.Add(tag);
            await userRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TagResponseDto>> GetFollowingTagsForUserByIdAsync(int userId)
        {
            var users = await userRepository.GetFollowingTagsForUserById(userId);
            return mapper.Map<IEnumerable<TagResponseDto>>(users);
        }

        public async Task UnfollowTagAsync(int userId, int tagId)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new BadRequestException($"there is no user with this id: {userId}");
            }
            var tag = await tagRepository.GetTagByIdAsync(tagId);
            if (tag == null)
            {
                throw new BadRequestException($"there is no tag with this id: {tagId}");
            }
            if (!user.Tags.Contains(tag))
            {
                throw new BadRequestException($"The user did't follow this tag");
            }
            user.Tags.Remove(tag);
            await userRepository.SaveChangesAsync();
        }
    }
}
