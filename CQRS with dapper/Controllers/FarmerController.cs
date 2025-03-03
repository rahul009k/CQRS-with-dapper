using CQRS_with_dapper.Data.Command;
using CQRS_with_dapper.Data.Query;
using CQRS_with_dapper.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CQRS_with_dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FarmerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<FarmerController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllFarmer());
            if (result is null) return Ok(new List<Farmer>());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFarmerById(int id)
        {
            var result = await _mediator.Send(new GetFarmerById(id));
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewFarmer(FarmerDto farmerDto)
        {
            var result = await _mediator.Send(new AddFarmerCommand(farmerDto.Name, farmerDto.Address, farmerDto.Phone_number));

            if (result == null)
                return BadRequest(new { message = "Failed to add farmer" }); 

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteFarmerById(int Id)
        {
            var result = await _mediator.Send(new DeleteFarmerCommand(Id));
            if (result)
            {
                return Ok(new { message = "Farmer Deleted successfully" });
            }
            else
            {
                return BadRequest(new { message = "Failed to delete the farmer" });
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateFarmerById(int Id,FarmerDto farmerDto)
        {
            var result = await _mediator.Send(new updateFarmerCommand(Id,farmerDto.Name,farmerDto.Address,farmerDto.Phone_number));
            if (result)
            {
                return Ok(new { message = "Farmer Updated successfully" });
            }
            else
            {
                return BadRequest(new { message = "Failed to Update the farmer" });
            }
        }


    }
}
