using AutoMapper;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.FollowingServices.Interfaces;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.FollowingServices.Implementations
{

    public class FollowingUsersServices : IFollowingUsersServices
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;

        public FollowingUsersServices(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork, IBasedRepositoryServices basedRepositoryServices)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
        }

        public async Task<IEnumerable<UserOverviewResponseDto>> GetFollowingUsersForUserByIdAsync(int userId)
        {
            var users = await userRepository.GetFollowingUsersForUserById(userId);
            return mapper.Map<IEnumerable<UserOverviewResponseDto>>(users);
        }

        public async Task FollowUserAsync(int userId, int followedUserId)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            var followedUser = await basedRepositoryServices.GetNonNullUserByIdAsync(followedUserId);
            if (userId == followedUserId)
            {
                throw new BadRequestException(UserExceptionMessages.CanNotFollowYourSelf);
            }
            if (user.FollowingUsers.Any(u => u.Id == followedUserId))
            {
                throw new BadRequestException(UserExceptionMessages.AlreadyFollowUser);
            }
            user.FollowingUsers.Add(followedUser);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UnfollowUserAsync(int userId, int followedUserId)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            var followedUser = await basedRepositoryServices.GetNonNullUserByIdAsync(followedUserId);
            if (!user.FollowingUsers.Contains(followedUser))
            {
                throw new BadRequestException(UserExceptionMessages.NotFollowedUser);
            }
            user.FollowingUsers.Remove(followedUser);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
