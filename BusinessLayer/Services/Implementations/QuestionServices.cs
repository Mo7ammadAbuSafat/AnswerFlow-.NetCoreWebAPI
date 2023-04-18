using AutoMapper;
using BusinessLayer.DTOs.AnswerDtos;
using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.DTOs.QuestionReportDtos;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        private readonly IQuestionReportRepository questionReportRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public QuestionServices
            (IQuestionRepository questionRepository,
            IUserRepository userRepository,
            ITagRepository tagRepository,
            IAnswerRepository answerRepository,
            IQuestionReportRepository questionReportRepository,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.questionRepository = questionRepository;
            this.userRepository = userRepository;
            this.tagRepository = tagRepository;
            this.answerRepository = answerRepository;
            this.questionReportRepository = questionReportRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<QuestionResponseDto>> GetAllQuestionsAsync()
        {
            var questions = await questionRepository.GetAllQuestionsAsync();
            return mapper.Map<IEnumerable<QuestionResponseDto>>(questions);
        }

        public async Task<IEnumerable<QuestionResponseDto>> GetFilteredQuestionsAsync
            (string? sortBy = null,
            DateTime? dateTime = null,
            QuestionStatus? status = null,
            ICollection<string>? tagNames = null
            )
        {
            IQueryable<Question> questions = await questionRepository.GetIQueryableQuestions();

            if (tagNames != null && tagNames.Count != 0)
            {
                questions = questions.Where(q => q.Tags.Any(t => tagNames.Contains(t.Name)));
            }
            if (dateTime != null)
            {
                questions = questions.Where(q => q.CreationDate >= dateTime);
            }
            if (status != null)
            {
                questions = questions.Where(q => q.Status == status);
            }
            if (sortBy != null)
            {
                switch (sortBy)
                {
                    case "date":
                        questions = questions.OrderByDescending(q => q.CreationDate);
                        break;
                    case "topVoted":
                        questions = questions.OrderByDescending(q => q.Votes.Count);
                        break;
                    case "topAnswered":
                        questions = questions.OrderByDescending(q => q.Answers.Count);
                        break;
                    default:
                        questions = questions = questions.OrderByDescending(q => q.CreationDate);
                        break;
                }
            }
            else questions = questions.OrderByDescending(q => q.CreationDate);

            var FilteredQuestions = await questions.ToListAsync();

            return mapper.Map<IEnumerable<QuestionResponseDto>>(FilteredQuestions);
        }

        public async Task<QuestionsWithPaginationResponseDto> GetFilteredQuestionsWithPaginationAsync
            (
            int pageNumber,
            int pageSize,
            string? sortBy = null,
            DateTime? dateTime = null,
            QuestionStatus? questionStatus = null,
            ICollection<string>? tagNames = null
            )
        {
            IQueryable<Question> questions = await questionRepository.GetIQueryableQuestions();

            if (tagNames != null && tagNames.Count != 0)
            {
                questions = questions.Where(q => q.Tags.Any(t => tagNames.Contains(t.Name)));
            }
            if (dateTime != null)
            {
                questions = questions.Where(q => q.CreationDate >= dateTime);
            }
            if (questionStatus != null)
            {
                questions = questions.Where(q => q.Status == questionStatus);
            }
            if (sortBy != null)
            {
                switch (sortBy)
                {
                    case "date":
                        questions = questions.OrderByDescending(q => q.CreationDate);
                        break;
                    case "topVoted":
                        questions = questions.OrderByDescending(q => q.Votes.Count);
                        break;
                    case "topAnswered":
                        questions = questions.OrderByDescending(q => q.Answers.Count);
                        break;
                    default:
                        questions = questions = questions.OrderByDescending(q => q.CreationDate);
                        break;
                }
            }
            else questions = questions.OrderByDescending(q => q.CreationDate);

            var numOfPages = Math.Ceiling(questions.Count() / (pageSize * 1f));
            if (pageNumber > numOfPages && numOfPages != 0)
            {
                throw new BadRequestException("num of pages is smaller than page number that you entered");
            }
            var finalQuestions = await questions
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();
            var result = new QuestionsWithPaginationResponseDto()
            {
                questions = mapper.Map<IEnumerable<QuestionResponseDto>>(finalQuestions),
                currentPage = pageNumber,
                numOfPages = (int)numOfPages
            };
            return result;
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

            questionToAddRequestDto.TagsNames = questionToAddRequestDto.TagsNames.Select(t => t.ToLower()).ToList();
            var tags = await tagRepository.GetTagsByNamesAsync(questionToAddRequestDto.TagsNames);

            var question = new Question()
            {
                Title = questionToAddRequestDto.Title,
                Body = questionToAddRequestDto.Body,
                Tags = tags,
                CreationDate = DateTime.Now,
                UserId = user.Id,
            };

            await questionRepository.AddAsync(question);

            await unitOfWork.SaveChangesAsync();

            return mapper.Map<QuestionResponseDto>(question);
        }

        public async Task<QuestionResponseDto> UpdateQuestionAsync(int questionId, QuestionUpdateRequestDto questionUpdateRequestDto)
        {
            var question = await questionRepository.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                throw new NotFoundException("No question with this id");
            }
            var dateNow = DateTime.Now;
            var editHistory = new QuestionHistory()
            {
                Title = question.Title,
                Body = question.Body,
                TagNames = string.Join(", ", question.Tags.Select(t => t.Name).ToArray()),
                EditDate = dateNow,
            };

            var tags = await tagRepository.GetTagsByNamesAsync(questionUpdateRequestDto.TagsNames);

            question.Title = questionUpdateRequestDto.Title;
            question.Body = questionUpdateRequestDto.Body;
            question.Tags = tags;
            question.LastEditDate = dateNow;
            question.EditHistory.Add(editHistory);

            await unitOfWork.SaveChangesAsync();

            return mapper.Map<QuestionResponseDto>(question);
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            var question = await questionRepository.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                throw new NotFoundException("No question with this id");
            }

            questionRepository.Delete(question);

            await unitOfWork.SaveChangesAsync();
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
            await unitOfWork.SaveChangesAsync();
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
            await unitOfWork.SaveChangesAsync();
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
            await unitOfWork.SaveChangesAsync();
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
            await unitOfWork.SaveChangesAsync();
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
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<AnswerResponseDto>(answer);
        }

        public async Task<AnswerResponseDto> UpdateAnswerAsync(int questionId, int answerId, AnswerUpdateRequestDto answerUpdateRequestDto)
        {
            var question = await questionRepository.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                throw new NotFoundException("No question with this id");
            }
            var answer = await answerRepository.GetAnswerByIdAsync(answerId);
            if (answer == null)
            {
                throw new NotFoundException("No answer with this id");
            }
            if (answer.QuestionId != questionId)
            {
                throw new BadRequestException("this answer doesn't for this question");
            }
            answer.Body = answerUpdateRequestDto.Body;

            await unitOfWork.SaveChangesAsync();

            return mapper.Map<AnswerResponseDto>(answer);
        }

        public async Task DeleteAnswerAsync(int questionId, int answerId)
        {
            var question = await questionRepository.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                throw new NotFoundException("No question with this id");
            }
            var answer = await answerRepository.GetAnswerByIdAsync(answerId);
            if (answer == null)
            {
                throw new NotFoundException("No answer with this id");
            }
            if (answer.QuestionId != questionId)
            {
                throw new BadRequestException("this answer doesn't for this question");
            }
            answerRepository.Delete(answer);

            await unitOfWork.SaveChangesAsync();
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

            if (!user.SavedQuestions.Any(c => c.Id == questionId))
            {
                user.SavedQuestions.Add(question);
                await unitOfWork.SaveChangesAsync();
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

            if (user.SavedQuestions.Any(c => c.Id == questionId))
            {
                user.SavedQuestions.Remove(question);
                await unitOfWork.SaveChangesAsync();
            }

        }

        public async Task ApproveAnswerAsync(int questionId, int answerId)
        {
            var question = await questionRepository.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                throw new NotFoundException("No question with this id");
            }
            var answer = await answerRepository.GetAnswerByIdAsync(answerId);
            if (answer == null)
            {
                throw new NotFoundException("No answer with this id");
            }
            if (answer.QuestionId != questionId)
            {
                throw new BadRequestException("this answer doesn't for this question");
            }

            answer.AnswerStatus = AnswerStatus.Approved;
            question.Status = QuestionStatus.Closed;
            await unitOfWork.SaveChangesAsync();
        }

        public async Task ReportQuestionAsync(int questionId, QuestionReportRequestDto questionReportRequestDto)
        {
            var user = await userRepository.GetUserById(questionReportRequestDto.UserId);
            if (user == null)
            {
                throw new NotFoundException("No user with this id");
            }
            var question = await questionRepository.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                throw new NotFoundException("No question with this id");
            }

            var questionReport = new QuestionReport()
            {
                Question = question,
                CreationDate = DateTime.Now,
                User = user,
                Description = questionReportRequestDto.Description,
            };
            question.Reports.Add(questionReport);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<QuestionReportResponseDto>> GetQuestionReportsAsync()
        {
            var reports = await questionReportRepository.GetQuestionReportsAsync();
            return mapper.Map<IEnumerable<QuestionReportResponseDto>>(reports);
        }
    }
}
