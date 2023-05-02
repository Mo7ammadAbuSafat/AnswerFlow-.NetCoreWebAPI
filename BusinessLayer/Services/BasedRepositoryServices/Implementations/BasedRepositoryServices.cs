using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.BasedRepositoryServices.Implementations
{
    public class BasedRepositoryServices : IBasedRepositoryServices
    {
        private readonly IUserRepository userRepository;
        private readonly ITagRepository tagRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly IAnswerRepository answerRepository;
        private readonly IAnswerReportRepository answerReportRepository;
        private readonly IQuestionReportRepository questionReportRepository;



        public BasedRepositoryServices(
            IUserRepository userRepository,
            IQuestionRepository questionRepository,
            IAnswerRepository answerRepository,
            ITagRepository tagRepository,
            IAnswerReportRepository answerReportRepository,
            IQuestionReportRepository questionReportRepository)
        {
            this.userRepository = userRepository;
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
            this.tagRepository = tagRepository;
            this.answerReportRepository = answerReportRepository;
            this.questionReportRepository = questionReportRepository;
        }


        public async Task<User> GetNonNullUserByIdAsync(int id)
        {
            var user = await userRepository.GetUserById(id);
            if (user == null)
            {
                throw new NotFoundException(UserExceptionMessages.NotFoundUserById);
            }
            return user;
        }

        public async Task<Question> GetNonNullQuestionByIdAsync(int id)
        {
            var question = await questionRepository.GetQuestionByIdAsync(id);
            if (question == null)
            {
                throw new NotFoundException(QuestionExceptionMessages.NotFoundQuestionById);
            }
            return question;
        }

        public async Task<Answer> GetNonNullAnswerByIdAsync(int id)
        {
            var answer = await answerRepository.GetAnswerByIdAsync(id);
            if (answer == null)
            {
                throw new NotFoundException(AnswerExceptionMessages.NotFoundAnswerById);
            }
            return answer;
        }

        public async Task<Tag> GetNonNullTagByIdAsync(int id)
        {
            var tag = await tagRepository.GetTagByIdAsync(id);
            if (tag == null)
            {
                throw new NotFoundException(TagExceptionMessages.NotFoundTagById);
            }
            return tag;
        }

        public async Task<AnswerReport> GetNonNullAnswerReportByIdAsync(int id)
        {
            var report = await answerReportRepository.GetAnswerReportByIdAsync(id);
            if (report == null)
            {
                throw new NotFoundException(ReportExceptionMessages.NotFoundReportById);
            }
            return report;
        }

        public async Task<QuestionReport> GetNonNullQuesitonReportByIdAsync(int id)
        {
            var report = await questionReportRepository.GetQuestionReportByIdAsync(id);
            if (report == null)
            {
                throw new NotFoundException(ReportExceptionMessages.NotFoundReportById);
            }
            return report;
        }

    }
}
