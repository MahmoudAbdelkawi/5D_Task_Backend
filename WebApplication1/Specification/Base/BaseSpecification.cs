using System.Linq.Expressions;

namespace WebApplication1.Specification.Base
{
    public class BaseSpecification<T> : IBaseSpecification<T>
    {
        public List<Expression<Func<T, bool>>> Criterias { get; } = new List<Expression<Func<T, bool>>>();
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public int PageSize { get; private set; }
        public int PageNumber { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        public BaseSpecification<T> AddCriteria(Expression<Func<T, bool>> criteria)
        {
            Criterias.Add(criteria);
            return this;
        }

        public BaseSpecification<T> AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
            return this;
        }

        public BaseSpecification<T> ApplyOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
            return this;
        }

        public BaseSpecification<T> ApplyOrderByDescending(Expression<Func<T, object>> orderByDescending)
        {
            OrderByDescending = orderByDescending;
            return this;
        }

        public BaseSpecification<T> ApplyPaging(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            IsPagingEnabled = true;
            return this;
        }
    }
}
