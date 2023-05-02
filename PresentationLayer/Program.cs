using BusinessLayer.Services.AnswerServices.Implementations;
using BusinessLayer.Services.AnswerServices.Interfaces;
using BusinessLayer.Services.BasedRepositoryServices.Implementations;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.FollowingServices.Implementations;
using BusinessLayer.Services.FollowingServices.Interfaces;
using BusinessLayer.Services.QuestionServices.Implementations;
using BusinessLayer.Services.QuestionServices.Interfaces;
using BusinessLayer.Services.ReportServices.Implementations;
using BusinessLayer.Services.ReportServices.Interfaces;
using BusinessLayer.Services.SaveQuestionServices.Implementations;
using BusinessLayer.Services.SaveQuestionServices.Interfaces;
using BusinessLayer.Services.TagServices.Implementations;
using BusinessLayer.Services.TagServices.Interfaces;
using BusinessLayer.Services.UserAccountServices.Implementations;
using BusinessLayer.Services.UserAccountServices.Interfaces;
using BusinessLayer.Services.VoteServices.Implementations;
using BusinessLayer.Services.VoteServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using PersistenceLayer.DbContexts;
using PersistenceLayer.Repositories.Implementations;
using PersistenceLayer.Repositories.Interfaces;
using PresentationLayer;
using PresentationLayer.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

//BasedRepositoriesServices
builder.Services.AddScoped<IBasedRepositoryServices, BasedRepositoryServices>();

//AnswerServices
builder.Services.AddScoped<IAnswerServices, AnswerServices>();

//ReportServices
builder.Services.AddScoped<IAnswerReportServices, AnswerReportServices>();
builder.Services.AddScoped<IQuestionReportServices, QuestionReportServices>();
builder.Services.AddScoped<IReportStatisticsServices, ReportStatisticsServices>();
builder.Services.AddScoped<IDualReportDataServices, DualReportDataServices>();

//FollowingServices
builder.Services.AddScoped<IFollowingQuestionsRetrievalServices, FollowingQuestionsRetrievalServices>();
builder.Services.AddScoped<IFollowingTagsServices, FollowingTagsServices>();
builder.Services.AddScoped<IFollowingUsersServices, FollowingUsersServices>();

//QuestionServices
builder.Services.AddScoped<IAddAndDeleteQuestionServices, AddAndDeleteQuestionServices>();
builder.Services.AddScoped<IQuestionRetrievalServices, QuestionRetrievalServices>();
builder.Services.AddScoped<IQuestionStatisticsServices, QuestionStatisticsServices>();
builder.Services.AddScoped<IUpdateQuestionServices, UpdateQuestionServices>();
builder.Services.AddScoped<IQuestionServicesFacade, QuestionServicesFacade>();

//SavedQuestionsServices
builder.Services.AddScoped<ISavedQuestionServices, SavedQuestionServices>();

//TagServices
builder.Services.AddScoped<ITagServices, TagServices>();

//VotesServices
builder.Services.AddScoped<IQuestionVoteServices, QuestionVoteServices>();
builder.Services.AddScoped<IAnswerVoteServices, AnswerVoteServices>();

//UserServices
builder.Services.AddScoped<IUserInformationServices, UserInformationServices>();
builder.Services.AddScoped<IUserStatisticsServices, UserStatisticsServices>();
builder.Services.AddScoped<IUserPermissionsServices, UserPermissionsServices>();
builder.Services.AddScoped<IUserRolesServices, UserRolesServices>();
builder.Services.AddScoped<IUserLoginServices, UserLoginServices>();
builder.Services.AddScoped<IUserRegistrationServices, UserRegistrationServices>();
builder.Services.AddScoped<IUserPasswordServices, UserPasswordServices>();
builder.Services.AddScoped<IUserServicesFacade, UserServicesFacade>();

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IQuestionReportRepository, QuestionReportRepository>();
builder.Services.AddScoped<IAnswerReportRepository, AnswerReportRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().
     AllowAnyHeader());
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AnswerFlowContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
