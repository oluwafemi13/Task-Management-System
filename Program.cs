using Management.Application.Contracts;
using Management.Infrastructure.Data;
using Management.Infrastructure.Repositories;
using Management.Infrastructure.Services;
using Management.Infrastructure.Services.Background_Services;
using Management.Infrastructure.Services.Business_Logic;
using Management.Infrastructure.Services.Worker;
using Management.Infrastructure.Services.Worker.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DatabaseContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IWorker, MarkedComplete>();
builder.Services.AddScoped<IWorker, UserTaskCompleted>();
builder.Services.AddScoped<IWorker, AssignedNewTask>();
builder.Services.AddHostedService<MarkedCompletedService>();
builder.Services.AddHostedService<AssignedNewTaskService>();
builder.Services.AddHostedService<MarkedUserTaskCompleted>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IUserservice, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
