using Mizan.Domain.Primitives;

namespace Mizan.Domain.Filters
{
    public sealed record FundsFilter : BaseFilter
    {
        public string? Name { get; set; }
    }
}
