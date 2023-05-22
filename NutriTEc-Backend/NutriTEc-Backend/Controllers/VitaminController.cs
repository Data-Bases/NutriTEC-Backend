using Microsoft.AspNetCore.Mvc;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Repository.Interface;
using System.ComponentModel.DataAnnotations;

namespace NutriTEc_Backend.Controllers
{
    [ApiController]
    [Route("nutritec/vitamin")]
    public class VitaminController : ControllerBase
    {
        private readonly INutriTEcRepository _repository;
        public VitaminController(INutriTEcRepository repository)
        {
           _repository = repository;
        }

        /// <summary>
        /// Get vitamins information
        /// </summary>
        /// <returns>
        /// Vitamin name
        /// Vitamin Id
        /// </returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetVitamins", Name = "GetVitamins")]
        public ActionResult<VitaminDto> GetVitamins()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vitamins = _repository.GetAllVitamins();

            return Ok(vitamins);
        }
    }
}