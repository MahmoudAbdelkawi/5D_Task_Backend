using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApplication1.Dtos;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Repository;
using WebApplication1.Specification;
using WebApplication1.Specification.Base;

namespace WebApplication1.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileUpload _fileUpload;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IFileUpload fileUpload, IMapper mapper)
        {
            _userRepository = userRepository;
            _fileUpload = fileUpload;
            _mapper = mapper;
        }

        [HttpGet("Users/")]
        public IActionResult GetUsers([FromQuery]BaseUserDto userDto, int pageSize = 10, int pageNumber = 1)
        {
            var users = _userRepository.GetUsersAsNoTracking();
            var filteredUsers = SpecificationEvaluator<User>.GetQuery(users, new UserSpecification(userDto.Name, userDto.Weight, userDto.DateOfBirth, pageSize, pageNumber)).ToList();
            foreach (var user in filteredUsers)
            {
                if (user.Photo != null)
                {
                    user.Photo = _fileUpload.GetFilePath(UploadDirectoriesEnum.UserPhotos, user.Photo);
                }
            }
            return Ok(filteredUsers);
        }

        [HttpGet("Users/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.Photo != null)
            user.Photo = _fileUpload.GetFilePath(UploadDirectoriesEnum.UserPhotos, user.Photo);
            return Ok(user);
        }

        [HttpPost("Users/")]
        public async Task<IActionResult> AddUser([FromForm]UserDto user)
        {
            await Console.Out.WriteLineAsync(JsonSerializer.Serialize(user));
            var userEntity = _mapper.Map<User>(user);
            if (user.Photo != null)
            {
                var (FileName, Extension) = _fileUpload.UploadFile(user.Photo, UploadDirectoriesEnum.UserPhotos);
                userEntity.Photo = $"{FileName}";
            }
            var addedUser = await _userRepository.AddUserAsync(userEntity);
            if (user.Photo != null)
            addedUser.Photo = _fileUpload.GetFilePath(UploadDirectoriesEnum.UserPhotos, addedUser.Photo);
            return StatusCode(201, addedUser);
        }

        [HttpPut("Users/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromForm]UserDto user)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            
            existingUser.Name = user.Name;
            existingUser.Weight = user.Weight;
            existingUser.DateOfBirth = user.DateOfBirth.Value;
            if (user.Photo != null)
            {
                var (FileName, Extension) = _fileUpload.UploadFile(user.Photo, UploadDirectoriesEnum.UserPhotos);
                existingUser.Photo = $"{FileName}";
            }

            await _userRepository.UpdateUserAsync(existingUser);
            if (user.Photo != null)
            {
                existingUser.Photo = _fileUpload.GetFilePath(UploadDirectoriesEnum.UserPhotos, existingUser.Photo);
            }
            return Ok(existingUser);
        }

        [HttpDelete("Users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userRepository.DeleteUserAsync(user);
            return NoContent();
        }

    }
}
