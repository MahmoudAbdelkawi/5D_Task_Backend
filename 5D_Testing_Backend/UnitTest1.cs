using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repository;
using Xunit;

namespace _5D_Testing_Backend
{
    public class UnitTest1
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;

        public UnitTest1()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "Test")
                    .Options;

            _context = new ApplicationDbContext(dbOptions);

            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public async Task TestAddUser()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Name = "Test User",
                DateOfBirth = new DateTime(1990, 1, 1),
                Photo = "test.jpg",
                Weight = "50",
            };

            // Act
            await _userRepository.AddUserAsync(user);

            // Assert
            var result = await _userRepository.GetUserByIdAsync(1);
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task TestDeleteUser()
        {
            // Arrange
            var user = new User
            {
                Id = 2,
                Name = "Test User",
                DateOfBirth = new DateTime(1990, 1, 1),
                Photo = "test.jpg",
                Weight = "50",
            };

            // Act
            await _userRepository.AddUserAsync(user);
            await _userRepository.DeleteUserAsync(user);

            // Assert
            var result = await _userRepository.GetUserByIdAsync(2);
            Assert.Null(result);
        }

        [Fact]
        public async Task TestUpdateUser()
        {
            // Arrange
            var user = new User
            {
                Id = 3,
                Name = "Test User",
                DateOfBirth = new DateTime(1990, 1, 1),
                Photo = "test.jpg",
                Weight = "50",
            };

            // Act
            await _userRepository.AddUserAsync(user);

            user.Name = "Updated User";

            await _userRepository.UpdateUserAsync(user);

            // Assert
            var result = await _userRepository.GetUserByIdAsync(3);
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task TestGetUserById()
        {
            // Arrange
            var user = new User
            {
                Id = 4,
                Name = "Test User",
                DateOfBirth = new DateTime(1990, 1, 1),
                Photo = "test.jpg",
                Weight = "50",
            };

            // Act
            await _userRepository.AddUserAsync(user);

            // Assert
            var result = await _userRepository.GetUserByIdAsync(4);
            Assert.Equal(user, result);
        }
    }
}