using AutoMapper;
using BusinessLayer.DTOs.TagDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.FollowingServices.Interfaces;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.FollowingServices.Implementations
{
    public class FollowingTagsServices : IFollowingTagsServices
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;
        private readonly IAuthenticatedUserServices authenticatedUserServices;

        public FollowingTagsServices(
            IUserRepository userRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IBasedRepositoryServices basedRepositoryServices,
            IAuthenticatedUserServices authenticatedUserServices)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
            this.authenticatedUserServices = authenticatedUserServices;
        }

        public async Task<IEnumerable<TagResponseDto>> GetFollowingTagsForUserByIdAsync(int userId)
        {
            var tags = await userRepository.GetFollowingTagsForUserById(userId);
            return mapper.Map<IEnumerable<TagResponseDto>>(tags);
        }

        public async Task FollowTagAsync(int userId, int tagId)
        {
            var authenticatedUserId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            if (authenticatedUserId != userId)
            {
                throw new UnauthorizedException();
            }
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            var tag = await basedRepositoryServices.GetNonNullTagByIdAsync(tagId);
            user.Tags.Add(tag);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UnfollowTagAsync(int userId, int tagId)
        {
            var authenticatedUserId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            if (authenticatedUserId != userId)
            {
                throw new UnauthorizedException();
            }
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            var tag = await basedRepositoryServices.GetNonNullTagByIdAsync(tagId);
            if (!user.Tags.Contains(tag))
            {
                throw new BadRequestException(TagExceptionMessages.NotFollowedTag);
            }
            user.Tags.Remove(tag);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
