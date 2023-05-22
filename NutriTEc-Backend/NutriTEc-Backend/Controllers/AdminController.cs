using Microsoft.AspNetCore.Mvc;
using Nest;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Helpers;
using NutriTEc_Backend.Repository.Interface;
using System.ComponentModel.DataAnnotations;

namespace NutriTEc_Backend.Controllers
{
    [ApiController]
    [Route("nutritec/admin")]
    public class AdminController : ControllerBase
    {
        private readonly INutriTEcRepository _repository;
        public AdminController(INutriTEcRepository repository)
        {
            _repository = repository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("AdminSignUp", Name = "AdminSignUp")]
        public ActionResult<Result> AdminSignUp(AdminDto admin)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.AdminSignUp(admin);

            if(result == Result.Error)
            {
                return Unauthorized();
            }

            return Ok();
            
        }
    }
}