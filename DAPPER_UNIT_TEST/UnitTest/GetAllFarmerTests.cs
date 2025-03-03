using CQRS_with_dapper.Controllers;
using CQRS_with_dapper.Data.Query;
using CQRS_with_dapper.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAPPER_UNIT_TEST.UnitTest
{
    public class GetAllFarmerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly FarmerController _farmerController;
        public GetAllFarmerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _farmerController = new FarmerController(_mockMediator.Object);
        }
        [Fact]
        public async Task GetallFarmer_returnsAll_true()
        {
            var mockFarmer = new List<Farmer?>()
            {
                new Farmer{Id=1,Name="rahul",Address="",Phone_number=""},
                new Farmer{Id=2,Name="rahul",Address="",Phone_number=""}
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllFarmer>(), default)).ReturnsAsync(mockFarmer!);
            var result = await _farmerController.GetAll();
            Assert.NotNull(result);
           

        }
        [Fact]
        public async Task GetAllFarmer_empty_db_true()
        {
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllFarmer>(), default)).ReturnsAsync(new List<Farmer?>());
            var result = await _farmerController.GetAll();
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<List<Farmer>>>(result);
            var value = actionResult.Value;
            Assert.NotNull(actionResult);
        }
    }
}
