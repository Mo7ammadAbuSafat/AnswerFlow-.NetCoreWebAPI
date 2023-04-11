using AutoMapper;
using BusinessLayer.DTOs.AnswerDtos;
using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.Interfaces;
using PersistenceLayer.Entities;
using PersistenceLayer.Enums;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.Implementations
{
    public class QuestionServices : IQuestionServices
    {

        private readonly IQuestionRepository questionRepository;
        private readonly IUserRepository userRepository;
        private readonly ITagRepository tagRepository;
        private readonly IAnswerRepository answerRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public QuestionServices(IQuestionRepository questionRepository, IUserRepository userRepository, ITagRepository tagRepository, IAnswerRepository answerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.questionRepository = questionRepository;
            this.userRepository = userRepository;
            this.tagRepository = tagRepository;
            this.answerRepository = answerRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<QuestionResponseDto>> GetAllQuestionsAsync()
        {
            var questions = await questionRepository.GetAllQuestionsAsync();
            return mapper.Map<IEnumerable<QuestionResponseDto>>(questions);
        }

        public async Task<QuestionResponseDto> GetQuestionByIdAsync(int questionId)
        {
            var question = await questionRepository.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                throw new NotFoundException("No question with this id");
            }
            return mapper.Map<QuestionResponseDto>(question);
        }

        public async Task<QuestionResponseDto> AddNewQuestionAsync(QuestionToAddRequestDto questionToAddRequestDto)
        {
            var user = await userRepository.GetUserById(questionToAddRequestDto.UserId);
            if (user == null)
            {
                throw new NotFoundException("No user with this id");
            }

            var question = new Question()
            {
                Title = questionToAddRequestDto.Title,
                Body = questionToAddRequestDto.Body,
                CreationDate = DateTime.Now,
                UserId = user.Id,
            };

            await addTags(question, questionToAddRequestDto.TagsNames);
            await questionRepository.AddAsync(question);

            await questionRepository.SaveChangesAsync();

            return mapper.Map<QuestionResponseDto>(question);
        }

        private async Task addTags(Question question, ICollection<string> tagNames)
        {
            foreach (string tagName in tagNames)
            {
                var tag = await tagRepository.GetTagByNameAsync(tagName);
                question.Tags.Add(tag);
            }
        }

        public async Task VoteForAQuestionAsync(int userId, int questionId, VoteType voteType)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new NotFoundException("No user with this id");
            }
            var question = await questionRepository.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                throw new NotFoundException("No question with this id");
            }
            var vote = question.Votes.Where(x => x.UserId == userId && x.QuestionId == questionId).FirstOrDefault();
            if (vote == null)
            {
                vote = new QuestionVote()
                {
                    QuestionId = questionId,
                    UserId = userId,
                    Type = voteType,
                    CreationDate = DateTime.Now
                };
                question.Votes.Add(vote);
            }
            else if (vote.Type != voteType)
            {
                vote.Type = voteType;
                vote.CreationDate = DateTime.Now;
            }
            await questionRepository.SaveChangesAsync();
        }

        public async Task DeleteVoteFromAQuestionAsync(int userId, int questionId)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new NotFoundException("No user with this id");
            }
            var question = await questionRepository.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                throw new NotFoundException("No question with this id");
            }
            var vote = question.Votes.Where(x => x.UserId == userId && x.QuestionId == questionId).FirstOrDefault();
            if (vote == null)
            {
                throw new NotFoundException("No vote with this ids");
            }
            question.Votes.Remove(vote);
            await questionRepository.SaveChangesAsync();
        }

        public async Task VoteForAnAnswerAsync(int userId, int answerId, VoteType voteType)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new NotFoundException("No user with this id");
            }
            var answer = await answerRepository.GetAnswerByIdAsync(answerId);
            if (answer == null)
            {
                throw new NotFoundException("No answer with this id");
            }
            var vote = answer.Votes.Where(x => x.UserId == userId && x.AnswerId == answerId).FirstOrDefault();
            if (vote == null)
            {
                vote = new AnswerVote()
                {
                    AnswerId = answerId,
                    UserId = userId,
                    Type = voteType,
                    CreationDate = DateTime.Now
                };
                answer.Votes.Add(vote);
            }
            else if (vote.Type != voteType)
            {
                vote.Type = voteType;
                vote.CreationDate = DateTime.Now;
            }
            await answerRepository.SaveChangesAsync();
        }

        public async Task DeleteVoteFromAnAnswerAsync(int userId, int answerId)
        {
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new NotFoundException("No user with this id");
            }
            var answer = await answerRepository.GetAnswerByIdAsync(answerId);
            if (answer == null)
            {
                throw new NotFoundException("No answer with this id");
            }
            var vote = answer.Votes.Where(x => x.UserId == userId && x.AnswerId == answerId).FirstOrDefault();
            if (vote == null)
            {
                throw new NotFoundException("No vote with this ids");
            }
            answer.Votes.Remove(vote);
            await answerRepository.SaveChangesAsync();
        }

        public async Task<AnswerResponseDto> AddNewAnswerAsync(int questionId, AnswerToAddRequestDto answerToAddRequestDto)
        {
            var user = await userRepository.GetUserById(answerToAddRequestDto.UserId);
            if (user == null)
            {
                throw new NotFoundException("No user with this id");
            }
            var question = await questionRepository.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                throw new NotFoundException("No question with this id");
            }
            var answer = new Answer()
            {
                UserId = user.Id,
                QuestionId = question.Id,
                Body = answerToAddRequestDto.Body,
                CreationDate = DateTime.Now,
            };
            question.Answers.Add(answer);
            await questionRepository.SaveChangesAsync();
            return mapper.Map<AnswerResponseDto>(answer);
        }

        public async Task SaveQuestionAsync(int userId, int questionId)
        {
            var question = await questionRepository.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                throw new NotFoundException("No question with this id");
            }
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new NotFoundException("No user with this id");
            }

            if (!user.SavedQuestions.Any(c => c.QuestionId == questionId))
            {
                var savedQuestion = new SavedQuestion()
                {
                    UserId = userId,
                    QuestionId = questionId,
                    CreationDate = DateTime.Now,
                };

                user.SavedQuestions.Add(savedQuestion);
                await userRepository.SaveChangesAsync();
            }



        }

        public async Task DeleteSavedQuestionAsync(int userId, int questionId)
        {
            var question = await questionRepository.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                throw new NotFoundException("No question with this id");
            }
            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new NotFoundException("No user with this id");
            }

            if (user.SavedQuestions.Any(c => c.QuestionId == questionId))
            {
                var savedQuestion = user.SavedQuestions.Where(s => s.QuestionId == questionId).FirstOrDefault();

                user.SavedQuestions.Remove(savedQuestion);
                await userRepository.SaveChangesAsync();
            }



        }

    }
}
