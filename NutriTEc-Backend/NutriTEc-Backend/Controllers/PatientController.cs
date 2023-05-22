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
    public class PatientController : ControllerBase
    {
        private readonly INutriTEcRepository _repository;
        public PatientController(INutriTEcRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Patient Sign up
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>Result</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("PatientSignUp", Name = "PatientSignUp")]
        public ActionResult<Result> PatientSignUp(PatientDto patient)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.PatientSignUp(patient);

            if(result == Result.Error)
            {
                return Unauthorized();
            }

            return Ok();
            
        }
    }
}