using CQRS_with_dapper.Controllers;
using CQRS_with_dapper.Data.Command;
using MediatR;
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
    public class DeleteFarmerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly FarmerController _farmerController;
        public DeleteFarmerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _farmerController = new FarmerController(_mockMediator.Object);
        }
        [Fact]
        public async Task DeleteFarmer_true()
        {
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteFarmerCommand>(), default)).ReturnsAsync(true);
            var result=await _farmerController.DeleteFarmerById(1);
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(okResult.Value);

            var jasonResult = JObject.FromObject(okResult.Value);
            Assert.Equal("Farmer Deleted successfully", jasonResult["message"]?.ToString());
        }
        [Fact]
        public async Task DeleteFarmer_False()
        {
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteFarmerCommand>(), default)).ReturnsAsync(false);
            var result=await _farmerController.DeleteFarmerById(1);
            var BadRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.NotNull(BadRequestResult.Value);

            var jasonResult = JObject.FromObject(BadRequestResult.Value);
            Assert.Equal("Failed to delete the farmer", jasonResult["message"]?.ToString());
        }
    }
}
