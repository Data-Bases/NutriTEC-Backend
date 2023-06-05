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

        /// <summary>
        /// Insert product to plan
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("InsertProductToPlan", Name = "InsertProductToPlan")]
        public ActionResult<Result> InsertProductToPlan([Required] int planId, ProductInPlanDto product)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipe = _repository.InsertProductToPlan(planId, product);

            if (recipe.Equals(Result.Error))
            {
                return Unauthorized();
            }

            return Ok(recipe);

        }

        /// <summary>
        /// Insert recipe to plan
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="recipe"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("InsertRecipeToPlan", Name = "InsertRecipeToPlan")]
        public ActionResult<Result> InsertRecipeToPlan([Required] int planId, RecipeInPlanDto recipe)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.InsertRecipeToPlan(planId, recipe);

            if (result.Equals(Result.Error))
            {
                return Unauthorized();
            }

            return Ok(result);

        }

        /// <summary>
        /// Edit product in plan
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("EditProductInPlan", Name = "EditProductInPlan")]
        public ActionResult<Result> EditProductInPlan([Required] int planId, ProductInPlanDto product)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.EditProductInPlan(planId, product);

            if (result.Equals(Result.NotFound))
            {   
                return NotFound();
            }

            if (result.Equals(Result.Error))
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        /// <summary>
        /// Edit recipe in plan
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="recipe"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("EditRecipeInPlan", Name = "EditRecipeInPlan")]
        public ActionResult<Result> EditRecipeInPlan([Required] int planId, RecipeInPlanDto recipe)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.EditRecipeInPlan(planId, recipe);

            if (result.Equals(Result.NotFound))
            {
                return NotFound();
            }

            if (result.Equals(Result.Error))
            {
                return Unauthorized();
            }

            return Ok(result);
        }


        /// <summary>
        /// Delete product in plan
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteProductInPlan", Name = "DeleteProductInPlan")]
        public ActionResult<Result> DeleteProductInPlan([Required] int planId, ProductInPlanDto product)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.DeleteProductInPlan(planId, product);

            if (result.Equals(Result.NotFound))
            {
                return NotFound();
            }

            if (result.Equals(Result.Error))
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        /// <summary>
        /// Delete recipe in plna
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="recipe"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteRecipeInPlan", Name = "DeleteRecipeInPlan")]
        public ActionResult<Result> DeleteRecipeInPlan([Required] int planId, RecipeInPlanDto recipe)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.DeleteRecipeInPlan(planId, recipe);

            if (result.Equals(Result.NotFound))
            {
                return NotFound();
            }

            if (result.Equals(Result.Error))
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        /// <summary>
        /// Delete plan and all its relations
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeletePlan", Name = "DeletePlan")]
        public ActionResult<Result> DeletePlan([Required] int planId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipe = _repository.DeletePlan(planId);

            if (recipe.Equals(Result.Error))
            {
                return NotFound();
            }

            return Ok(recipe);

        }
    }
}