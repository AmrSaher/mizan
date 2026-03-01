using Ardalis.Specification;
using Mizan.Domain.Entities.FundDomain;
using Mizan.Domain.Filters;

namespace Mizan.Domain.Specifications
{
    public class FundLookupsSpecification : Specification<Fund>
    {
        public FundLookupsSpecification(FundLookupsFilter filter)
        {
            Query.AsNoTracking();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                Query.Where(x => x.Name.ToLower().Contains(filter.Name.Trim().ToLower()));
            }
        }
    }
}
