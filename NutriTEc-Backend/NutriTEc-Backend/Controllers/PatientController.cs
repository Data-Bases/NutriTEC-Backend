using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<Result> AddProductToPatient(PatientProductDto patientProductDto)
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
        public ActionResult<Result> AddRecipeToPatient(PatientRecipeDto patientRecipeDto)
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
        public ActionResult<Result> RegisterPatientMeasurements(int patientId, MeasurementDto measurementDto)
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
        /// Deleting a product from a patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="productId"></param>
        /// <returns>Result</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("DeleteProductFromPatient", Name = "DeleteProductFromPatient")]
        public ActionResult<Result> DeleteProductFromPatient([Required] int patientId, [Required] int productId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.DeleteProductFromPatient(patientId, productId);

            if (result == Result.Error)
            {
                return Unauthorized();
            }

            return Ok();

        }

        /// <summary>
        /// Deleting a recipe from a patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="recipeId"></param>
        /// <returns>Result</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("DeleteRecipeFromPatient", Name = "DeleteRecipeFromPatient")]
        public ActionResult<Result> DeleteRecipeFromPatient([Required] int patientId, [Required] int recipeId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.DeleteRecipeFromPatient(patientId, recipeId);

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
        [HttpGet("GetPatientsNutritionist", Name = "GetPatientsNutritionist")]
        public ActionResult<NutriIdDto> GetPatientsNutritionist(int patientId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = _repository.GetPatientsNutritionist(patientId);

            return Ok(products);
        }
    }
}