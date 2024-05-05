using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Specification.Base
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, IBaseSpecification<T> specification)
        {
            var query = inputQuery;

            if (specification.Criterias.Count > 0)
            {
                foreach (var criteria in specification.Criterias)
                {
                    query = query.Where(criteria);
                }
            }

            if (specification.Includes.Count > 0)
            {
                foreach (var include in specification.Includes)
                {
                    query = query.Include(include);
                }
            }

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.IsPagingEnabled)
            {
                query = query.Skip((specification.PageNumber - 1) * specification.PageSize)
                    .Take(specification.PageSize);
            }

            return query;
        }
    }
}
