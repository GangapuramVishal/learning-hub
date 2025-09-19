using Application.Features.Cars.Commands;
using Application.Features.Cars.Queries;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : Controller
    {
        private readonly ISender _mediatrSender;


        public CarsController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewProperty([FromBody] NewCar newCarRequest)
        {
            bool isSuccessful = await _mediatrSender.Send(new CreateCarRequest(newCarRequest));
            if(isSuccessful)
            {
                return Ok("Car created successfully.");
            }
            return BadRequest("Failed to create Car");
        }

        [HttpPut("updateCar")]
        public async Task<IActionResult> UpdateCar([FromBody] UpdateCar updateCar)
        {
            bool isSuccessfu = await _mediatrSender.Send(new UpdateCarRequest(updateCar));
            if (isSuccessfu)
            {
                return Ok("Car Updated Successfully.");
            }
            return NotFound("Car does not exists.");
        }

        [HttpGet("{id}")]
        
        public async Task<IActionResult>GetCarById(int id)
        {
            CarDto carDto = await _mediatrSender.Send(new GetCarByIdRequest(id));
            if(carDto != null)
            {
                return Ok(carDto);
            }
            return NotFound("Car does not exists.");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetCars()
        {
            List<CarDto> carDtos = await _mediatrSender.Send(new GetCarRequest());
            if(carDtos != null)
            {
                return Ok(carDtos);
            }
            return NotFound("No Cars were found.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteCar(int id)
        {
            bool isSuccessful = await _mediatrSender.Send(new DeleteCarRequest(id));
            if (isSuccessful)
            {
                return Ok("Car deleted successfully.");
            }
            return NotFound("Car does not exists.");
        }
    }
}
