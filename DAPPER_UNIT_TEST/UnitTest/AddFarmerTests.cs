using CQRS_with_dapper.Controllers;
using CQRS_with_dapper.Data.Command;
using CQRS_with_dapper.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAPPER_UNIT_TEST.UnitTest
{
    public class AddFarmerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly FarmerController _farmerController;
        public AddFarmerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _farmerController = new FarmerController(_mockMediator.Object);
        }
        [Fact]
        public async Task AddNewFarmer_true()
        {
            var newFarmer = new FarmerDto()
            {
                Name = "rahul",
                Address = "Ranchi",
                Phone_number = ""
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<AddFarmerCommand>(), default)).ReturnsAsync(newFarmer);
            var result = await _farmerController.AddNewFarmer(newFarmer);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            var returnedFarmer = Assert.IsType<FarmerDto>(okResult.Value);
            Assert.Equal(newFarmer.Name, returnedFarmer.Name);
            Assert.Equal(newFarmer.Address, returnedFarmer.Address);
        }
        [Fact]
        public async Task AddNewFarmer_ReturnsBadRequest_WhenFails()
        {
            // Arrange
            var newFarmer = new FarmerDto()
            {
                Name = "Rahul",
                Address = "Ranchi",
                Phone_number = "7788994455"
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<AddFarmerCommand>(), default)).ReturnsAsync((FarmerDto?)null);
            var result = await _farmerController.AddNewFarmer(newFarmer);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);

            var jasonResult = JObject.FromObject(badRequestResult.Value);
            Assert.Equal("Failed to add farmer", jasonResult["message"]?.ToString());
            
        }
    }
}
