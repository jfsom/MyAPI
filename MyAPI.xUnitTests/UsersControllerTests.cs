using Microsoft.AspNetCore.Mvc;
using Moq;
using MyAPI.Controllers;
using MyAPI.Models;

namespace MyAPI.xUnitTests
{
    public class UsersControllerTests
    {
        // Instance of the controller being tested
        private readonly UsersController _controller;

        // Mock instance of the service
        private readonly Mock<IUserService> _mockService;

        // Constructor to initialize mock service and the controller
        public UsersControllerTests()
        {
            // Creating a mock object for IUserService
            _mockService = new Mock<IUserService>();

            // Injecting the mock object into the controller
            _controller = new UsersController(_mockService.Object);
        }

        // Test to verify that GetUserById returns an OkObjectResult with the correct user
        [Fact]
        public void GetUser_ReturnsOkResultWithUser()
        {
            // Arrange
            var userId = 1; // Define the user ID to retrieve
            var expectedUser = new User { Id = 1, Name = "John Doe", Email = "john@example.com" }; // Define the expected user
            _mockService.Setup(service => service.GetUserById(userId)).Returns(expectedUser); // Configure the mock service to return the expected user

            // Act
            var result = _controller.GetUserById(userId) as OkObjectResult; // Call the controller method and cast the result to OkObjectResult

            // Assert
            Assert.NotNull(result); // Check that the result is not null
            Assert.Equal(200, result.StatusCode); // Check that the status code is 200 OK
            Assert.Equal(expectedUser, result.Value); // Check that the returned value is the expected user
        }

        // Test to verify that GetUserById returns a NotFoundResult when the user is not found
        [Fact]
        public void GetUser_ReturnsNotFoundWhenUserNotFound()
        {
            // Arrange
            var userId = 99; // Define a non-existent user ID
            _mockService.Setup(service => service.GetUserById(userId)).Returns((User)null); // Configure the mock service to return null

            // Act
            var result = _controller.GetUserById(userId) as NotFoundResult; // Call the controller method and cast the result to NotFoundResult

            // Assert
            Assert.NotNull(result); // Check that the result is not null
            Assert.Equal(404, result.StatusCode); // Check that the status code is 404 Not Found
        }

        // Test to verify that GetAllUsers returns an OkObjectResult with a list of users
        [Fact]
        public void GetAllUsers_ReturnsOkResultWithListOfUsers()
        {
            // Arrange
            var expectedUsers = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
            }; // Define the expected list of users
            _mockService.Setup(service => service.GetAllUsers()).Returns(expectedUsers); // Configure the mock service to return the expected list of users

            // Act
            var result = _controller.GetAllUsers() as OkObjectResult; // Call the controller method and cast the result to OkObjectResult

            // Assert
            Assert.NotNull(result); // Check that the result is not null
            Assert.Equal(200, result.StatusCode); // Check that the status code is 200 OK
            Assert.Equal(expectedUsers, result.Value); // Check that the returned value is the expected list of users
        }

        // Test to verify that AddUser returns a CreatedAtActionResult
        [Fact]
        public void AddUser_ReturnsCreatedAtAction()
        {
            // Arrange
            var newUser = new User { Id = 3, Name = "Sam Wilson", Email = "sam@example.com" }; // Define the new user to add

            // Act
            var result = _controller.AddUser(newUser) as CreatedAtActionResult; // Call the controller method and cast the result to CreatedAtActionResult

            // Assert
            Assert.NotNull(result); // Check that the result is not null
            Assert.Equal(201, result.StatusCode); // Check that the status code is 201 Created
            Assert.Equal("GetUserById", result.ActionName); // Check that the action name is "GetUserById"
            Assert.Equal(newUser.Id, ((User)result.Value).Id); // Check that the returned user ID is the new user's ID
        }

        // Test to verify that UpdateUser returns a NoContentResult
        [Fact]
        public void UpdateUser_ReturnsNoContent()
        {
            // Arrange
            var updatedUser = new User { Id = 1, Name = "John Updated", Email = "john.updated@example.com" }; // Define the updated user
            _mockService.Setup(service => service.UpdateUser(updatedUser)); // Configure the mock service to update the user

            // Act
            var result = _controller.UpdateUser(1, updatedUser) as NoContentResult; // Call the controller method and cast the result to NoContentResult

            // Assert
            Assert.NotNull(result); // Check that the result is not null
            Assert.Equal(204, result.StatusCode); // Check that the status code is 204 No Content
        }

        // Test to verify that DeleteUser returns a NoContentResult
        [Fact]
        public void DeleteUser_ReturnsNoContent()
        {
            // Arrange
            var userId = 1; // Define the user ID to delete
            _mockService.Setup(service => service.DeleteUser(userId)); // Configure the mock service to delete the user

            // Act
            var result = _controller.DeleteUser(userId) as NoContentResult; // Call the controller method and cast the result to NoContentResult

            // Assert
            Assert.NotNull(result); // Check that the result is not null
            Assert.Equal(204, result.StatusCode); // Check that the status code is 204 No Content
        }
    }
}