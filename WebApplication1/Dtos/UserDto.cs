using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos
{
    public class UserDto : BaseUserDto
    {
        public IFormFile? Photo { get; set; }
    }
}
