using Hangfire;
using Mizan.Application.Interfaces;
using Mizan.IoC;
using Mizan.API.Middlewares;
using Mizan.Infrastructure.Scraper.Scrapers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddInfrastructureScraper(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", corsBuilder =>
    {
        corsBuilder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

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

using (var scraperScope = app.Services.CreateScope())
{
    var azimutScraper = scraperScope.ServiceProvider.GetRequiredService<AzimutScraper>();
    var beltoneScraper = scraperScope.ServiceProvider.GetRequiredService<BeltoneScraper>();
    var ciCapitalScraper = scraperScope.ServiceProvider.GetRequiredService<CICapitalScraper>();
    var efgHermesScraper = scraperScope.ServiceProvider.GetRequiredService<EFGHermesScraper>();
    var egxStocksScraper = scraperScope.ServiceProvider.GetRequiredService<EGXStocksScraper>();

    //await azimutScraper.Run();
    //await beltoneScraper.Run();
    //await ciCapitalScraper.Run();
    //await efgHermesScraper.Run();
    //await egxStocksScraper.Run();
}

app.Run();
