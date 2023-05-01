using AutoMapper;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.UserAccountServices.Interfaces;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.UserAccountServices.Implementations
{
    public class UserInformationServices : IUserInformationServices
    {

        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;


        public UserInformationServices(
            IUserRepository userRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IBasedRepositoryServices basedRepositoryServices)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
        }

        public async Task<UserOverviewResponseDto> UpdateUserInformationAsync(int userId, UserInformationToUpdateRequestDto userInformationDto)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
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
                throw new NotFoundException(UserExceptionMessages.NotFoundUserByEmail);
            }
            return mapper.Map<UserOverviewResponseDto>(user);
        }

        public async Task<FullUserResponseDto> GetFullUserByIdAsync(int userId)
        {
            var user = await userRepository.GetFullUserById(userId);
            if (user == null)
            {
                throw new NotFoundException(UserExceptionMessages.NotFoundUserById);
            }
            return mapper.Map<FullUserResponseDto>(user);
        }
    }
}
