using CoffeeMachineAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CoffeeMachineAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using CoffeeMachineAPI.Models;
using Microsoft.AspNetCore.Http;

namespace CoffeeMachineAPI.Test
{
    public class CoffeeMachineAPIControllerTest
    {
        internal CoffeeMachineAPIController? controller;
        internal Mock<ICoffeeMachine> mockCoffeeMachine;
        internal Mock<IDateTimeProvider> mockDateProvider;
        internal Mock<IWeatherChecker> mockWeatherChecker;

        public CoffeeMachineAPIControllerTest()
        {
            mockCoffeeMachine = new();
            mockDateProvider = new();
            mockWeatherChecker = new();
        }
        [Fact]
        public async void Successful_brew_coffee_GET_request_return_OK()
        {
            // Arrange
            mockCoffeeMachine.Setup(m => m.IsBrewSuccess()).Returns(true);
            mockDateProvider.Setup(m => m.Now()).Returns(new DateTime(2024, 3, 2, 0, 0, 0));
            mockWeatherChecker.Setup(m => m.GetTempAsync()).ReturnsAsync(17.0f);
            controller = new CoffeeMachineAPIController(mockCoffeeMachine.Object, 
                                                        mockDateProvider.Object,
                                                        mockWeatherChecker.Object);
            // Act
            var result = await controller.GetAsync();
            // Assert that the return type is of type OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Assert that the status code is 200 OK
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            // Assert that the response body is of type APIResponse
            var apiResponse = Assert.IsType<APIResponse>(okResult.Value);
            // Assert that the message property of the APIResponse object is correct
            Assert.Equal("Your piping hot coffee is ready", apiResponse.message);
            // Assert that the prepared property of the APIResponse object is of type DateTime
            Assert.IsType<DateTime>(apiResponse.prepared);
            // Assert how many times IsBrewSuccess method was called on the mockCoffeeMachine object
            mockCoffeeMachine.Verify(m => m.IsBrewSuccess(), Times.Once);
        }
        [Fact]
        public async void Coffee_out_of_stock_GET_request_return_service_Unavailable()
        {
            // Arrange
            mockCoffeeMachine.Setup(m => m.IsBrewSuccess()).Returns(false);
            mockDateProvider.Setup(m => m.Now()).Returns(new DateTime(2024, 3, 2, 0, 0, 0));
            mockWeatherChecker.Setup(m => m.GetTempAsync()).ReturnsAsync(17.0f);
            var controller = new CoffeeMachineAPIController(mockCoffeeMachine.Object,
                                                            mockDateProvider.Object,
                                                            mockWeatherChecker.Object);
            // Act
            var result = await controller.GetAsync();
            // Assert that the return type is of type ObjectResult
            var objResult = Assert.IsType<ObjectResult>(result.Result);
            // Assert that the status code is 503
            Assert.Equal(StatusCodes.Status503ServiceUnavailable, objResult.StatusCode);
            // Assert that the response body is empty
            Assert.Equivalent(new { }, objResult.Value);
            // Assert how many times IsBrewSuccess method was called on the mockCoffeeMachine object
            mockCoffeeMachine.Verify(m => m.IsBrewSuccess(), Times.Once);
        }
        [Fact]
        public async void April_first_return_teapot()
        {
            // Arrange
            mockCoffeeMachine.Setup(m => m.IsBrewSuccess()).Returns(true);
            mockDateProvider.Setup(m => m.Now()).Returns(new DateTime(2024, 4, 1, 0, 0, 0));
            mockWeatherChecker.Setup(m => m.GetTempAsync()).ReturnsAsync(17.0f);
            var controller = new CoffeeMachineAPIController(mockCoffeeMachine.Object,
                                                            mockDateProvider.Object,
                                                            mockWeatherChecker.Object);
            // Act
            var result = await controller.GetAsync();
            // Assert that the return type is of type ObjectResult
            var objResult = Assert.IsType<ObjectResult>(result.Result);
            // Assert that the status code is 418
            Assert.Equal(StatusCodes.Status418ImATeapot, objResult.StatusCode);
            // Assert that the response body is empty
            Assert.Equivalent(new { }, objResult.Value);
            // Assert how many times IsBrewSuccess method was called on the mockCoffeeMachine object
            mockCoffeeMachine.Verify(m => m.IsBrewSuccess(), Times.Never);
        }
        [Fact]
        public async void Successful_brew_ice_coffee_GET_request_return_OK()
        {
            // Arrange
            mockCoffeeMachine.Setup(m => m.IsBrewSuccess()).Returns(true);
            mockDateProvider.Setup(m => m.Now()).Returns(new DateTime(2024, 3, 2, 0, 0, 0));
            mockWeatherChecker.Setup(m => m.GetTempAsync()).ReturnsAsync(32.0f);
            controller = new CoffeeMachineAPIController(mockCoffeeMachine.Object,
                                                        mockDateProvider.Object,
                                                        mockWeatherChecker.Object);
            // Act
            var result = await controller.GetAsync();
            // Assert that the return type is of type OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            // Assert that the status code is 200 OK
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            // Assert that the response body is of type APIResponse
            var apiResponse = Assert.IsType<APIResponse>(okResult.Value);
            // Assert that the message property of the APIResponse object is correct
            Assert.Equal("Your refreshing iced coffee is ready", apiResponse.message);
            // Assert that the prepared property of the APIResponse object is of type DateTime
            Assert.IsType<DateTime>(apiResponse.prepared);
            // Assert how many times IsBrewSuccess method was called on the mockCoffeeMachine object
            mockCoffeeMachine.Verify(m => m.IsBrewSuccess(), Times.Once);
        }
    }
}
