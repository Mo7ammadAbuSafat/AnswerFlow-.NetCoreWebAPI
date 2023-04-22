using BusinessLayer.Services.Implementations;
using BusinessLayer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using PersistenceLayer.DbContexts;
using PersistenceLayer.Repositories.Implementations;
using PersistenceLayer.Repositories.Interfaces;
using PresentationLayer;
using PresentationLayer.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IQuestionReportRepository, QuestionReportRepository>();
builder.Services.AddScoped<IAnswerReportRepository, AnswerReportRepository>();
builder.Services.AddScoped<ITagServices, TagServices>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionServices, QuestionServices>();
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
