using AutoMapper;
using BusinessLayer.DTOs.StatisticsDtos;
using BusinessLayer.DTOs.TagDtos;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using PersistenceLayer.Entities;
using PersistenceLayer.Enums;
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
        private readonly IUnitOfWork unitOfWork;

        public UserServices(IUserRepository userRepository, IMapper mapper, ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.tagRepository = tagRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserOverviewResponseDto> RegisterUserAsync(UserRegistrationRequestDto userRegistration)
        {

            if (userRepository.CheckIfEmailExists(userRegistration.Email))
            {
                throw new BadRequestException($"this email is already exist");
            }

            CreatePasswordHash(userRegistration.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User()
            {
                Username = userRegistration.Username,
                Email = userRegistration.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreationDate = DateTime.Now,
                VerificationCode = CreateRandomCode()
            };
            SendEmailWithCode(user.Email, "Verification Account", user.VerificationCode);
            await userRepository.AddAsync(user);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<UserOverviewResponseDto>(user);
        }

        public async Task<UserOverviewResponseDto> LoginUserAsync(UserLoginRequestDto userLogin)
        {
            var user = await userRepository.GetUserByEmail(userLogin.Email);
            if (user == null)
            {
                throw new BadRequestException("this email is not exist");
            }

            if (!VerifyPassword(userLogin.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new BadRequestException($"Password is not correct");
            }

            if (user.VerifiedDate == null)
            {
                throw new BadRequestException($"You must Verify your email");
            }
            return mapper.Map<UserOverviewResponseDto>(user);
        }

        public async Task<UserOverviewResponseDto> VerifyEmailAsync(int userId, string code)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new BadRequestException("No user with this id");
            }
            if (code != user.VerificationCode)
            {
                throw new BadRequestException("Invalid code");
            }
            user.VerifiedDate = DateTime.Now;
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<UserOverviewResponseDto>(user);
        }

        public async Task ResendVerificationCodeAsync(int userId)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new BadRequestException("No user with this id");
            }
            if (user.VerificationCode == null)
            {
                user.VerificationCode = CreateRandomCode();
                await unitOfWork.SaveChangesAsync();
            }
            SendEmailWithCode(user.Email, "Verification Account", user.VerificationCode);
        }

        public async Task<UserOverviewResponseDto> SendResetPasswordCodeAsync(string email)
        {
            var user = await userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new BadRequestException("This email is not exist");
            }
            user.ResetPasswordCode = CreateRandomCode();
            user.ResetPasswordCodeExpiresDate = DateTime.Now.AddDays(1);
            await unitOfWork.SaveChangesAsync();
            SendEmailWithCode(email, "Reset Password", user.ResetPasswordCode);
            return mapper.Map<UserOverviewResponseDto>(user);
        }

        public async Task ResendResetPasswordCodeAsync(int userId)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new BadRequestException("No user with this id");
            }
            user.ResetPasswordCode = CreateRandomCode();
            user.ResetPasswordCodeExpiresDate = DateTime.Now.AddDays(1);
            await unitOfWork.SaveChangesAsync();
            SendEmailWithCode(user.Email, "Reset Password", user.ResetPasswordCode);
        }

        public async Task ResetPasswordByCodeSendedToEmailAsync(int userId, ResetPasswordWithCodeRequestDto resetPasswordDto)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new BadRequestException("No user with this id");
            }
            if (user.ResetPasswordCode != resetPasswordDto.Code)
            {
                throw new BadRequestException("Invalid Code");
            }
            if (user.ResetPasswordCodeExpiresDate < DateTime.Now)
            {
                await ResendResetPasswordCodeAsync(user.Id);
                throw new BadRequestException("Expired Code, we sent another one");
            }

            CreatePasswordHash(resetPasswordDto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ResetPasswordCode = null;
            await userRepository.SaveChangesAsync();
        }

        public async Task ResetPasswordByOldPasswordAsync(int userId, ResetPasswordWithOldPasswordRequestDto resetPasswordDto)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new BadRequestException("No user with this id");
            }
            if (!VerifyPassword(resetPasswordDto.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                throw new BadRequestException($"Password is not correct");
            }

            CreatePasswordHash(resetPasswordDto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await unitOfWork.SaveChangesAsync();
        }

        private static void SendEmailWithCode(string email, string subject, string code)
        {
            var Host = Configration.config["Smtp:Host"];
            var UserName = Configration.config["Smtp:UserName"];
            var FromEmail = Configration.config["Smtp:FromEmail"];
            var Password = Configration.config["Smtp:Password"];
            var Port = Convert.ToInt16(Configration.config["Smtp:Port"]);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(UserName, FromEmail));
            message.To.Add(new MailboxAddress("user", email));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = $"{subject} code : {code} , copy it and send it"

            };

            using (var client = new SmtpClient())
            {
                client.Connect(Host, Port, false);
                client.Authenticate(FromEmail, Password);
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

        private static string CreateRandomCode()
        {
            Random rnd = new();
            int num = rnd.Next();
            return num.ToString();
        }

        public async Task<UserOverviewResponseDto> UpdateUserInformationAsync(int userId, UserInformationToUpdateRequestDto userInformationDto)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new BadRequestException("No user with this id");
            }
            user.Username = userInformationDto.Username;
            user.About = userInformationDto.About;
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<UserOverviewResponseDto>(user);
        }

        public async Task<IEnumerable<UserOverviewResponseDto>> GetUsersAsync()
        {
            var users = await userRepository.GetUsers();
            return mapper.Map<IEnumerable<UserOverviewResponseDto>>(users);
        }

        public async Task<UserOverviewResponseDto> GetUserByEmailAsync(string email)
        {
            var user = await userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new NotFoundException("No user with this email");
            }
            return mapper.Map<UserOverviewResponseDto>(user);
        }

        public async Task<FullUserResponseDto> GetFullUserByIdAsync(int userId)
        {
            var user = await userRepository.GetFullUserById(userId);
            if (user == null)
            {
                throw new NotFoundException("No user with this id");
            }
            return mapper.Map<FullUserResponseDto>(user);
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
            await unitOfWork.SaveChangesAsync();
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
            await unitOfWork.SaveChangesAsync();
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
            await unitOfWork.SaveChangesAsync();
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
            await unitOfWork.SaveChangesAsync();
        }

        public async Task BlockUserFromPostingAsync(int userId)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new BadRequestException("there is no user with this id");
            }
            if (user.IsBlockedFromPosting == true)
            {
                throw new BadRequestException("the user already  blocked");
            }
            if (user.Type == UserType.Expert || user.Type == UserType.Admin)
            {
                throw new BadRequestException("you can't block this user");
            }

            user.IsBlockedFromPosting = true;
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UnblockUserFromPostingAsync(int userId)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new BadRequestException("there is no user with this id");
            }
            if (user.IsBlockedFromPosting == false)
            {
                throw new BadRequestException("the user already  unblocked");
            }

            user.IsBlockedFromPosting = false;
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpgradeUserToExpertAsync(int userId)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new BadRequestException("there is no user with this id");
            }
            if (user.Type == UserType.Expert)
            {
                throw new BadRequestException("the user already expert");
            }
            if (user.Type == UserType.Admin)
            {
                throw new BadRequestException("the user is admin");
            }

            user.Type = UserType.Expert;
            user.IsBlockedFromPosting = false;
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpgradeUserToAdminAsync(int userId)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new BadRequestException("there is no user with this id");
            }
            if (user.Type == UserType.Admin)
            {
                throw new BadRequestException("the user is already admin");
            }

            user.Type = UserType.Admin;
            user.IsBlockedFromPosting = false;
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<UsersStatisticsResponseDto> GetUsersStatisticsAsync()
        {
            var users = await userRepository.GetUsers();
            var statistics = new UsersStatisticsResponseDto()
            {
                UsersCount = users.Count(),
                ExpertUsersCount = users.Where(q => q.Type == UserType.Expert).Count(),
                LastMonthAddedUsersCount = users.Where(q => q.CreationDate >= DateTime.Now.AddMonths(-1)).Count(),
                LastMonthAddedExpertsCount = users.Where(q => q.CreationDate >= DateTime.Now.AddMonths(-1) && q.Type == UserType.Expert).Count(),
            };
            return statistics;
        }
    }
}
