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
    public class NutritionistController : ControllerBase
    {
        private readonly INutriTEcRepository _repository;
        public NutritionistController(INutriTEcRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Nutritionist sign up
        /// </summary>
        /// <param name="nutri"></param>
        /// <returns>Result</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("NutritionistSignUp", Name = "NutritionistSignUp")]
        public ActionResult<Result> NutritionistSignUp(NutriDto nutri)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.NutriSignUp(nutri);

            if(result == Result.Error)
            {
                return Unauthorized();
            }

            return Ok();
            
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetPatientsByNutriId", Name = "GetPatientsByNutriId")]
        public ActionResult<ProductDto> GetPatientsByNutriId(int nutriId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = _repository.GetPatientsByNutriId(nutriId);

            return Ok(products);  
        }
    }
}