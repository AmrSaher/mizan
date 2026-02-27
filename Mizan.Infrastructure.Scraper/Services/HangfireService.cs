using Hangfire;
using Mizan.Application.Interfaces;
using System.Linq.Expressions;

namespace Mizan.Infrastructure.Scraper.Services
{
    public class HangfireService : IBackgroundJobService
    {
        public string Enqueue(Expression<Action> methodCall)
        {
            return BackgroundJob.Enqueue(methodCall);
        }

        public string Schedule(Expression<Action> methodCall, TimeSpan delay)
        {
            return BackgroundJob.Schedule(methodCall, delay);
        }

        public void ScheduleRecurringJob(string jobId, Expression<Action> methodCall, string cronExpression)
        {
            RecurringJob.AddOrUpdate(jobId, methodCall, cronExpression);
        }
    }
}
