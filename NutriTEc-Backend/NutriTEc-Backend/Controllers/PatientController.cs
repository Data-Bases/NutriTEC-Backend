using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nest;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Helpers;
using NutriTEc_Backend.Repository.Interface;
using System.ComponentModel.DataAnnotations;

namespace NutriTEc_Backend.Controllers
{
    [ApiController]
    [Route("nutritec/patient")]
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
        public ActionResult<Result> PatientSignUp([Required] PatientDto patient)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.PatientSignUp(patient);

            if (result == Result.Error)
            {
                return Unauthorized();
            }

            return Ok();

        }
        /// <summary>
        /// Adding a Product to a patient
        /// </summary>
        /// <param name="patientProductDto"></param>
        /// <returns>Result</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("AddProductToPatient", Name = "AddProductToPatient")]
        public ActionResult<Result> AddProductToPatient([Required] PatientProductDto patientProductDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.AddProductToPatient(patientProductDto);

            if (result == Result.Error)
            {
                return Unauthorized();
            }

            return Ok();

        }
        /// <summary>
        /// Adding a Recipe to a patient
        /// </summary>
        /// <param name="patientRecipeDto"></param>
        /// <returns>Result</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("AddRecipeToPatient", Name = "AddRecipeToPatient")]
        public ActionResult<Result> AddRecipeToPatient([Required] PatientRecipeDto patientRecipeDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.AddRecipeToPatient(patientRecipeDto);

            if (result == Result.Error)
            {
                return Unauthorized();
            }

            return Ok();

        }

        /// <summary>
        /// Asociating a plan to a patient
        /// </summary>
        /// <param name="planPatientDto"></param>
        /// <returns>Result</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("AddPlanToPatient", Name = "AddPlanToPatient")]
        public ActionResult<Result> AddPlanToPatient([Required] PlanPatientDto planPatientDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.AddPlanToPatient(planPatientDto);

            if (result == Result.Error)
            {
                return Unauthorized();
            }

            return Ok();

        }

        /// <summary>
        /// Registering a patient's measurements on an specific date
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="measurementDto"></param>
        /// <param name="revisionDate"></param>
        /// <returns>Result</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("RegisterPatientMeasurements", Name = "RegisterPatientMeasurements")]
        public ActionResult<Result> RegisterPatientMeasurements([Required] int patientId, MeasurementDto measurementDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.RegisterPatientMeasurements(patientId, measurementDto);

            if (result == Result.Error)
            {
                return Unauthorized();
            }

            return Ok();

        }

        /// <summary>
        /// Getting a patients nutricionist id and name
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>The id and name of the nutricionist</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetNutritionistByPatiendId/{id}", Name = "GetNutritionistByPatiendId/{id}")]
        public ActionResult<NutriIdDto> GetPatientsNutritionist([Required] int patientId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = _repository.GetPatientsNutritionist(patientId);

            return Ok(products);
        }

        /// <summary>
        /// Get daily consumption by a patient on a certain date
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="dateConsumed"></param>
        /// <returns>DailyConsumptionDto</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetDailyConsumptionByPatient", Name = "GetDailyConsumptionByPatient")]
        public ActionResult<DailyConsumptionDto> GetDailyConsumptionByPatient([Required] int patientId, [Required] DateTime dateConsumed)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dailyConsumption = _repository.GetDailyConsumptionByPatient(patientId, dateConsumed);

            if (dailyConsumption.TotalCalories == 0)
            {
                return NotFound();
            }

            return Ok(dailyConsumption);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetPatientMeasurementsByDate", Name = "GetPatientMeasurementsByDate")]
        public ActionResult<MeasurementDto> GetPatientMeasurementsByDate([Required] int patientId, [Required] DateTime startDate, [Required] DateTime finishDate)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var measurements = _repository.GetPatientMeasurementsByDate(patientId, startDate, finishDate);

            if (measurements.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(measurements);
        }
    }
}