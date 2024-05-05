using System.Linq.Expressions;

namespace WebApplication1.Specification.Base
{
    public interface IBaseSpecification<T>
    {
        public List<Expression<Func<T, bool>>> Criterias { get; }
        public List<Expression<Func<T, object>>> Includes { get; }
        public Expression<Func<T, object>> OrderBy { get; }
        public Expression<Func<T, object>> OrderByDescending { get; }
        public int PageSize { get; }
        public int PageNumber { get; }
        public bool IsPagingEnabled { get; }
    }
}
