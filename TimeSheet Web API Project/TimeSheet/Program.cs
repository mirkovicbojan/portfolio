using Microsoft.EntityFrameworkCore;
using TimeSheet.Contexts;
using TimeSheet.Repository;
using TimeSheet.Repository.Interfaces;
using TimeSheet.Services.Interfaces;
using TimeSheet.Services;
using NLog;
using TimeSheet.Contracts;
using TimeSheet.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<TimeSheetContext>(
    o => o.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseLazyLoadingProxies()
);

builder.Services.AddScoped<ITimeSheetRepository, TimeSheetRepository>();
builder.Services.AddScoped<ITimeSheetService, TimeSheetService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
