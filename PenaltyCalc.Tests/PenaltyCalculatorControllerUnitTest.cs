using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using PenaltyCalc.Controllers;
using PenaltyCalc.Data;
using PenaltyCalc.Models;
using PenaltyCalc.Tests.Models;
using Xunit;

namespace PenaltyCalc.Tests
{
    public class PenaltyCalculatorControllerTests
    {
        private readonly PenaltyCalculatorController _controller;
        private readonly Mock<ICountrySettingsRepository> _repositoryMock;

        public PenaltyCalculatorControllerTests()
        {
            _repositoryMock = new Mock<ICountrySettingsRepository>();
            _controller = new PenaltyCalculatorController(_repositoryMock.Object);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenCountrySettingsNotFound()
        {
            // Arrange
            Guid countryId = new Guid();
            var request = new PenaltyCalculate
            {
                selectedCountry = countryId,
                checkoutDate = new DateTime(2022, 1, 1),
                returnDate = new DateTime(2022, 1, 15)
            };

            _repositoryMock
                .Setup(r => r.GetCountrySettingsById(countryId))
                .ReturnsAsync((CountrySettings)null);

            // Act
            var result = await _controller.Post(request);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            string error = JsonConvert.SerializeObject(badRequestObjectResult.Value);
            Assert.Equal("{\"message\":\"Not meta-data found for the selected country. Unable to process request at the moment, please try later.\"}", error);
        }

        [Fact]
        public async Task Post_ReturnsOk_WithZeroPenaltyAmountAndDays()
        {
            // Arrange
            Guid countryId = new Guid();
            var request = new PenaltyCalculate
            {
                selectedCountry = countryId,
                checkoutDate = new DateTime(2022, 1, 1),
                returnDate = new DateTime(2022, 1, 15)
            };

            var countrySettings = new CountrySettings
            {
                id = countryId,
                weekendDays = "Saturday,Sunday",
                otherHolidays = "2022-01-05,2022-01-10",
                penaltyAmount = 5
            };


            _repositoryMock
                .Setup(r => r.GetCountrySettingsById(countryId))
                .ReturnsAsync(countrySettings);

            // Act
            var result = await _controller.Post(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Console.WriteLine();
            var value = Assert.IsType<PenaltyResponse>(JsonConvert.DeserializeObject<PenaltyResponse>(JsonConvert.SerializeObject(okObjectResult.Value)));
            Assert.Equal(0, value.penalty);
            Assert.Equal(new List<DateTime>(), value.days);
        }

        [Fact]
        public async Task Post_ReturnsOk_WithPenaltyAmountAndDays()
        {
            // Arrange
            Guid countryId = new Guid();
            var request = new PenaltyCalculate
            {
                selectedCountry = countryId,
                checkoutDate = new DateTime(2022, 1, 1),
                returnDate = new DateTime(2022, 1, 25)
            };

            var countrySettings = new CountrySettings
            {
                id = countryId,
                weekendDays = "Saturday,Sunday",
                otherHolidays = "2022-01-05,2022-01-10",
                penaltyAmount = 5
            };


            _repositoryMock
                .Setup(r => r.GetCountrySettingsById(countryId))
                .ReturnsAsync(countrySettings);

            // Act
            var result = await _controller.Post(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Console.WriteLine();
            var value = Assert.IsType<PenaltyResponse>(JsonConvert.DeserializeObject<PenaltyResponse>(JsonConvert.SerializeObject(okObjectResult.Value)));
            Assert.Equal((decimal)25.0, value.penalty);
            var penaltyDates = JsonConvert.DeserializeObject<List<DateTime>>("[\"2022-01-19T00:00:00\",\"2022-01-20T00:00:00\",\"2022-01-21T00:00:00\",\"2022-01-24T00:00:00\",\"2022-01-25T00:00:00\"]");
            Assert.Equal(penaltyDates, value.days);
        }
    }
}