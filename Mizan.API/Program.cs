using Hangfire;
using Mizan.Application.Interfaces;
using Mizan.Infrastructure.Scraper;
using Mizan.IoC;
using Mizan.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddInfrastructureScraper(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var jobService = scope.ServiceProvider.GetRequiredService<IBackgroundJobService>();

    //jobService.ScheduleRecurringJob(
    //    jobId: "daily-system-cleanup",
    //    methodCall: () => scope.ServiceProvider.GetRequiredService<ISystemCleanupJob>().ExecuteAsync(),
    //    cronExpression: Cron.Daily()
    //);
}

//await EGXStocks.Run();
//await Azimut.Run();
//await Beltone.Run();
//await CICapital.Run();
//await EFGHermes.Run();

app.Run();
