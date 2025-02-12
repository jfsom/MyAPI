using MyAPI.Models;
namespace MyAPI.xUnitTests
{
    public class UserRepositoryTests
    {
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            // Initialize the UserRepository with dummy data
            _userRepository = new UserRepository();
        }

        // Combined test to verify that GetUserById returns the correct user or null if not found
        [Theory]
        [InlineData(1, true)]  // User with ID 1 exists
        [InlineData(99, false)] // User with ID 99 does not exist
        public async Task GetUserById_ReturnsExpectedResult(int userId, bool userExists)
        {
            // Act
            var result = await _userRepository.GetUserByIdAsync(userId);

            // Assert
            if (userExists)
            {
                Assert.NotNull(result);
                Assert.Equal(userId, result.Id);
            }
            else
            {
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsAllUsers()
        {
            // Act
            var result = await _userRepository.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count()); // Assuming there are 2 users initially
        }

        [Fact]
        public async Task AddUserAsync_AddsUserCorrectly()
        {
            // Arrange
            var newUser = new User { Id = 3, Name = "Sam Wilson", Email = "sam@example.com" };

            // Act
            await _userRepository.AddUserAsync(newUser);
            var result = await _userRepository.GetUserByIdAsync(newUser.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newUser.Id, result.Id);
            Assert.Equal(newUser.Name, result.Name);
            Assert.Equal(newUser.Email, result.Email);
        }

        [Fact]
        public async Task UpdateUserAsync_UpdatesUserCorrectly()
        {
            // Arrange
            var updatedUser = new User { Id = 1, Name = "John Updated", Email = "john.updated@example.com" };

            // Act
            await _userRepository.UpdateUserAsync(updatedUser);
            var result = await _userRepository.GetUserByIdAsync(updatedUser.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedUser.Name, result.Name);
            Assert.Equal(updatedUser.Email, result.Email);
        }

        [Fact]
        public async Task DeleteUserAsync_DeletesUserCorrectly()
        {
            // Arrange
            var userId = 1;

            // Act
            await _userRepository.DeleteUserAsync(userId);
            var result = await _userRepository.GetUserByIdAsync(userId);

            // Assert
            Assert.Null(result);
        }
    }
}