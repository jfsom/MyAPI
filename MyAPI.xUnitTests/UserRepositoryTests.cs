// Importing the Models namespace which likely contains the UserRepository and User classes
using MyAPI.Models;

namespace MyAPI.xUnitTests
{
    public class UserRepositoryTests
    {
        private readonly UserRepository _userRepository;

        // Constructor initializes the UserRepository instance
        public UserRepositoryTests()
        {
            _userRepository = new UserRepository();
        }

        // Test to verify that GetUserById returns the correct user
        [Fact]
        public void GetUserById_ReturnsCorrectUser()
        {
            // Arrange
            var userId = 1;

            // Act
            var result = _userRepository.GetUserById(userId);

            // Assert
            // Check that result is not null
            Assert.NotNull(result);

            // Check that the ID of the returned user is correct
            Assert.Equal(userId, result.Id);
        }

        // Test to verify that GetUserById returns null when the user is not found
        [Fact]
        public void GetUserById_ReturnsNullWhenUserNotFound()
        {
            // Arrange
            // Assuming this ID does not exist
            var userId = 99;

            // Act
            var result = _userRepository.GetUserById(userId);

            // Assert
            // Check that result is null
            Assert.Null(result);
        }

        // Test to verify that GetAllUsers returns all users
        [Fact]
        public void GetAllUsers_ReturnsAllUsers()
        {
            // Act
            var result = _userRepository.GetAllUsers();

            // Assert
            // Check that result is not null
            Assert.NotNull(result);

            // Assuming there are 2 users, check that the count is correct
            Assert.Equal(2, result.Count());
        }

        // Test to verify that AddUser adds a user correctly
        [Fact]
        public void AddUser_AddsUserCorrectly()
        {
            // Arrange
            var newUser = new User { Id = 3, Name = "Sam Wilson", Email = "sam@example.com" };

            // Act
            _userRepository.AddUser(newUser);
            var result = _userRepository.GetUserById(3);

            // Assert
            Assert.NotNull(result); // Check that the user was added and returned
            Assert.Equal(newUser.Id, result.Id); // Check that the ID is correct
            Assert.Equal(newUser.Name, result.Name); // Check that the name is correct
            Assert.Equal(newUser.Email, result.Email); // Check that the email is correct
        }

        // Test to verify that UpdateUser updates a user correctly
        [Fact]
        public void UpdateUser_UpdatesUserCorrectly()
        {
            // Arrange
            var updatedUser = new User { Id = 1, Name = "John Updated", Email = "john.updated@example.com" };

            // Act
            _userRepository.UpdateUser(updatedUser);
            var result = _userRepository.GetUserById(1);

            // Assert
            Assert.NotNull(result); // Check that the user was found
            Assert.Equal(updatedUser.Name, result.Name); // Check that the name was updated
            Assert.Equal(updatedUser.Email, result.Email); // Check that the email was updated
        }

        // Test to verify that DeleteUser deletes a user correctly
        [Fact]
        public void DeleteUser_DeletesUserCorrectly()
        {
            // Arrange
            var userId = 1;

            // Act
            _userRepository.DeleteUser(userId);
            var result = _userRepository.GetUserById(userId);

            // Assert
            Assert.Null(result); // Check that the user was deleted and cannot be found
        }
    }
}