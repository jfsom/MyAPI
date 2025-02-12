using MyAPI.Models;

// Importing Moq for creating mock objects
using Moq;

namespace MyAPI.xUnitTests
{
    public class UserServiceTests
    {
        // Instance of the service being tested
        private readonly UserService _demoService;

        // Mock instance of the repository
        private readonly Mock<IUserRepository> _mockRepository;

        // Constructor to initialize mock repository and the service
        public UserServiceTests()
        {
            _mockRepository = new Mock<IUserRepository>();
            _demoService = new UserService(_mockRepository.Object);
        }

        // Test to verify that GetUserById returns the correct user
        [Fact]
        public void GetUserById_ReturnsUser()
        {
            // Arrange
            var userId = 1;
            var expectedUser = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };
            _mockRepository.Setup(repo => repo.GetUserById(userId)).Returns(expectedUser);

            // Act
            var result = _demoService.GetUserById(userId);

            // Assert
            Assert.NotNull(result); // Check that the result is not null
            Assert.Equal(expectedUser.Id, result.Id); // Check that the ID is correct
            Assert.Equal(expectedUser.Name, result.Name); // Check that the name is correct
            Assert.Equal(expectedUser.Email, result.Email); // Check that the email is correct
        }

        // Test to verify that GetUserById returns null when the user is not found
        [Fact]
        public void GetUserById_ReturnsNullWhenUserNotFound()
        {
            // Arrange
            var userId = 99;
            _mockRepository.Setup(repo => repo.GetUserById(userId)).Returns((User)null);

            // Act
            var result = _demoService.GetUserById(userId);

            // Assert
            Assert.Null(result); // Check that the result is null
        }

        // Test to verify that GetAllUsers returns a list of users
        [Fact]
        public void GetAllUsers_ReturnsListOfUsers()
        {
            // Arrange
            var expectedUsers = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
            };
            _mockRepository.Setup(repo => repo.GetAllUsers()).Returns(expectedUsers);

            // Act
            var result = _demoService.GetAllUsers();

            // Assert
            Assert.NotNull(result); // Check that the result is not null

            // Check that the count of users is correct
            Assert.Equal(expectedUsers.Count, result.Count());
        }

        // Test to verify that AddUser calls the repository's AddUser method
        [Fact]
        public void AddUser_CallsRepository()
        {
            // Arrange
            var newUser = new User { Id = 3, Name = "Sam Wilson", Email = "sam@example.com" };

            // Act
            _demoService.AddUser(newUser);

            // Assert
            // Check that AddUser was called once
            _mockRepository.Verify(repo => repo.AddUser(newUser), Times.Once);
        }

        // Test to verify that UpdateUser calls the repository's UpdateUser method
        [Fact]
        public void UpdateUser_CallsRepository()
        {
            // Arrange
            var updatedUser = new User { Id = 1, Name = "John Updated", Email = "john.updated@example.com" };

            // Act
            _demoService.UpdateUser(updatedUser);

            // Assert
            // Check that UpdateUser was called once
            _mockRepository.Verify(repo => repo.UpdateUser(updatedUser), Times.Once);
        }

        // Test to verify that DeleteUser calls the repository's DeleteUser method
        [Fact]
        public void DeleteUser_CallsRepository()
        {
            // Arrange
            var userId = 1;

            // Act
            _demoService.DeleteUser(userId);

            // Assert
            // Check that DeleteUser was called once
            _mockRepository.Verify(repo => repo.DeleteUser(userId), Times.Once);
        }
    }
}