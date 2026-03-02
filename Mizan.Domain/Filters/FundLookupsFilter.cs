using Mizan.Domain.Primitives;

namespace Mizan.Domain.Filters
{
    public sealed record FundLookupsFilter : BaseFilter
    {
        public string? Name { get; set; }
    }
}
