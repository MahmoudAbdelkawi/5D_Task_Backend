using WebApplication1.Models;
using WebApplication1.Specification.Base;

namespace WebApplication1.Specification
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(string? name, string? weight, DateTime? dateOfBirth, int pageSize = 10, int pageNumber = 1)
        {
            if (!string.IsNullOrEmpty(name))
            {
                AddCriteria(x => x.Name.Contains(name) || name.Contains(x.Name));
            }
            if (!string.IsNullOrEmpty(weight))
            {
                AddCriteria(x => x.Weight == weight);
            }
            if (dateOfBirth.HasValue)
            {
                AddCriteria(x => x.DateOfBirth == dateOfBirth);
            }
            ApplyPaging(pageNumber, pageSize);
        }
    }
}
