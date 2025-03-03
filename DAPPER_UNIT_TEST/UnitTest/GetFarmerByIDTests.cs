using CQRS_with_dapper.Controllers;
using CQRS_with_dapper.Data.Query;
using CQRS_with_dapper.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAPPER_UNIT_TEST.UnitTest
{
    public class GetFarmerByIDTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly FarmerController _farmerController;
        public GetFarmerByIDTests()
        {
            _mockMediator = new Mock<IMediator>();
            _farmerController = new FarmerController(_mockMediator.Object);

        }
        [Fact]
        public async Task GetFarmerById_true()
        {
            var farmer = new Farmer()
            {
                Id = 1,
                Name = "rahul",
                Address = "ranchi",
                Phone_number = "7788994455"
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetFarmerById>(), default)).ReturnsAsync(farmer);
            var acctionResult = await _farmerController.GetFarmerById(farmer.Id);
            var objresult = acctionResult as OkObjectResult;
            Assert.NotNull(objresult);
            Assert.IsType<Farmer>(objresult.Value);
            var result = objresult.Value as Farmer;
            Assert.Equal(farmer.Id,result?.Id);
            Assert.Equal(farmer.Name,result?.Name);


        }
        [Fact]
        public async Task GetFarmerById_false()
        {
           
            _mockMediator.Setup(m => m.Send(It.IsAny<GetFarmerById>(), default)).ReturnsAsync(default(Farmer?));
            var result = await _farmerController.GetFarmerById(2);
            var objresult = result as OkObjectResult;
            Assert.Null(objresult?.Value);
        }
    }
}
