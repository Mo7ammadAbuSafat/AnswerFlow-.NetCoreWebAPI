using BusinessLayer.DTOs.StatisticsDtos;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.UserAccountServices.Interfaces;
using PersistenceLayer.Entities;
using PersistenceLayer.Enums;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.UserAccountServices.Implementations
{
    public class UserStatisticsServices : IUserStatisticsServices
    {

        private readonly IUserRepository userRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly IAnswerRepository answerRepository;
        private readonly IBasedRepositoryServices basedRepositoryServices;

        public UserStatisticsServices(
            IUserRepository userRepository,
            IQuestionRepository questionRepository,
            IAnswerRepository answerRepository,
            IBasedRepositoryServices basedRepositoryServices)
        {
            this.userRepository = userRepository;
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
            this.basedRepositoryServices = basedRepositoryServices;
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

        public async Task<IEnumerable<string>> GetUserActivityCurrentYearStatisticAsync(int userId)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            return await userRepository.GetUserActivityCurrentYearStatistic(userId);
        }

        public async Task<UserStatisticsResponseDto> GetUserStatisticsAsync(int userId)
        {
            int approvedAnswersCount = await answerRepository.ApprovedAnswersCountForUserById(userId);
            IQueryable<Question> questions = await questionRepository.GetIQueryableQuestions();
            var userQuestionsCount = questions.Where(q => q.UserId == userId).Count();
            var lastMonthQuestionsCount = questions.Where(q => q.CreationDate >= DateTime.Now.AddMonths(-1)).Count();
            var statistics = new UserStatisticsResponseDto()
            {
                ApprovedAnswersCount = approvedAnswersCount,
                QuestionsCount = userQuestionsCount,
                LastMonthQuestionsCount = lastMonthQuestionsCount,
            };
            return statistics;
        }
    }
}
