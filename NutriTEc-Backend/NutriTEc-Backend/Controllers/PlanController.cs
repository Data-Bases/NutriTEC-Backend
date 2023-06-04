using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nest;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Helpers;
using NutriTEc_Backend.Models;
using NutriTEc_Backend.Repository.Interface;
using System.ComponentModel.DataAnnotations;

namespace NutriTEc_Backend.Controllers
{
    [ApiController]
    [Route("nutritec/plan")]
    public class PlanController : ControllerBase
    {
        private readonly INutriTEcRepository _repository;
        public PlanController(INutriTEcRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Create Plan
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns>Result</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("CreatePlan", Name = "CreatePlan")]
        public ActionResult<Result> CreatePlan([Required] PlanDto plan)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.CreatePlan(plan);

            if (result.Equals(Result.Error))
            {
                return Unauthorized();
            }

            return Ok();

        }

        /// <summary>
        /// Gets plan by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DailyConsumptionPlanDto</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetPlanById/{id}", Name = "GetPlanById/{id}")]
        public ActionResult<DailyConsumptionPlanDto> GetPlanById([Required] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.GetPlanById(id);

            if (result.Equals(new DailyConsumptionPlanDto()))
            {
                return NotFound();
            }

            return Ok(result);

        }

        /// <summary>
        /// Gets plan by patient Id and intial date
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>DailyConsumptionPlanDto</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetPlanByPatientId", Name = "GetPlanByPatientId")]
        public ActionResult<DailyConsumptionPlanDto> GetPlanByPatientId([Required] int patientId, [Required] DateTime initialDate)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.GetPlanByPatientId(patientId, initialDate);

            if (result.Equals(new DailyConsumptionPlanDto()))
            {
                return NotFound();
            }

            return Ok(result);

        }
    }
}