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
    [Route("nutritec/recipe")]
    public class RecipeController : ControllerBase
    {
        private readonly INutriTEcRepository _repository;
        public RecipeController(INutriTEcRepository repository)
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
        [HttpPost("CreateRecipe", Name = "CreateRecipe")]
        public ActionResult<Result> CreateRecipe([Required] RecipeXProductsDto recipe)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.CreateRecipe(recipe);

            if(result == Result.Error)
            {
                return Forbid();
            }

            return Ok();
            
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetRecipes", Name = "GetRecipes")]
        public ActionResult<List<RecipeDto>> GetRecipes()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipe = _repository.GetRecipes();

            if (recipe.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(recipe);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetRecipesById/{id}", Name = "GetRecipesById/{id}")]
        public ActionResult<RecipeInfoDto> GetRecipesById([Required] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipeServings = 1;

            var recipe = _repository.GetRecipeById(id, recipeServings);

            if (recipe.Equals(new RecipeInfoDto()))
            {
                return NotFound();
            }

            return Ok(recipe);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetRecipesByIdAndServings", Name = "GetRecipesByIdAndServings")]
        public ActionResult<RecipeInfoDto> GetRecipeByIdAndServings([Required] int id, [Required] int servings)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipe = _repository.GetRecipeById(id, servings);

            if (recipe.Equals(new RecipeInfoDto()))
            {
                return NotFound();
            }

            return Ok(recipe);

        }
    }
}