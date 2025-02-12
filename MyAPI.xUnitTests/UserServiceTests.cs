using MyAPI.Models;
using Moq;

namespace MyAPI.xUnitTests
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _mockRepository;

        public UserServiceTests()
        {
            // Create a mock repository
            _mockRepository = new Mock<IUserRepository>();
            // Inject the mock repository into the UserService
            _userService = new UserService(_mockRepository.Object);
        }

        [Theory]
        [MemberData(nameof(GetUserByIdData))]
        public async Task GetUserByIdAsync_ReturnsExpectedResult(int userId, User expectedUser)
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            if (expectedUser != null)
            {
                Assert.NotNull(result);
                Assert.Equal(expectedUser.Id, result.Id);
                Assert.Equal(expectedUser.Name, result.Name);
                Assert.Equal(expectedUser.Email, result.Email);
            }
            else
            {
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsListOfUsers()
        {
            // Arrange
            var expectedUsers = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
            };
            _mockRepository.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(expectedUsers);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUsers.Count, result.Count());
        }

        [Fact]
        public async Task AddUserAsync_CallsRepository()
        {
            // Arrange
            var newUser = new User { Id = 3, Name = "Sam Wilson", Email = "sam@example.com" };

            // Act
            await _userService.AddUserAsync(newUser);

            // Assert
            _mockRepository.Verify(repo => repo.AddUserAsync(newUser), Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_CallsRepository()
        {
            // Arrange
            var updatedUser = new User { Id = 1, Name = "John Updated", Email = "john.updated@example.com" };

            // Act
            await _userService.UpdateUserAsync(updatedUser);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateUserAsync(updatedUser), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_CallsRepository()
        {
            // Arrange
            var userId = 1;

            // Act
            await _userService.DeleteUserAsync(userId);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteUserAsync(userId), Times.Once);
        }

        // Data provider for GetUserByIdAsync test cases
        public static IEnumerable<object[]> GetUserByIdData =>
            new List<object[]>
            {
                new object[] { 1, new User { Id = 1, Name = "John Doe", Email = "john@example.com" } },
                new object[] { 99, null }
            };
    }
}