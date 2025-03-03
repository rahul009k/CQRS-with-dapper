using CQRS_with_dapper.Controllers;
using CQRS_with_dapper.Data.Command;
using CQRS_with_dapper.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using Octokit.Internal;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAPPER_UNIT_TEST.UnitTest
{
    public class UpdateFarmerTest
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly FarmerController _farmerController;
        public UpdateFarmerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _farmerController = new FarmerController(_mockMediator.Object);

        }
        [Fact]
        public async Task UpdateFarmer_True()
        {
            var farmer = new FarmerDto()
            {

                Name = "rahul",
                Address = "",
                Phone_number = ""
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<updateFarmerCommand>(), default)).ReturnsAsync(true);
            var result = await _farmerController.UpdateFarmerById(1, farmer);
            var okresult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okresult.Value);
            var jsonResponse = JObject.FromObject(okresult.Value); 
            Assert.Equal("Farmer Updated successfully", jsonResponse["message"]?.ToString());
        }
        [Fact]
        public async Task UpdateFarmer_False()
        {
            _mockMediator.Setup(m => m.Send(It.IsAny<updateFarmerCommand>(), default)).ReturnsAsync(false);
            var result = await _farmerController.UpdateFarmerById(1, new FarmerDto());
            var BadRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(BadRequestResult.Value);
            var jsonResponse = JObject.FromObject(BadRequestResult.Value);
            Assert.Equal("Failed to Update the farmer", jsonResponse["message"]?.ToString());
        }
    }
}
