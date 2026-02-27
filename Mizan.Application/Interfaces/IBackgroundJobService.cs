using System.Linq.Expressions;

namespace Mizan.Application.Interfaces
{
    public interface IBackgroundJobService
    {
        string Enqueue(Expression<Action> methodCall);
        string Schedule(Expression<Action> methodCall, TimeSpan delay);
        void ScheduleRecurringJob(string jobId, Expression<Action> methodCall, string cronExpression);
    }
}
