using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
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

        /// <summary>
        /// Get recipes
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get recipe by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns a recipe by id, and servings with the nutritional value 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="servings"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetRecipesByIdAndServings", Name = "GetRecipesByIdAndServings")]
        public ActionResult<RecipeInfoDto> GetRecipeByIdAndServings([Required] int id, [Required] double servings)
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

        /// <summary>
        /// Inserts a new product into a certain recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="productId"></param>
        /// <param name="servings"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("InsertProductToRecipe", Name = "InsertProductToRecipe")]
        public ActionResult<Result> InsertProductToRecipe([Required] int recipeId, [Required] int productId, [Required] double servings)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipe = _repository.InsertProductToRecipe(recipeId, productId, servings);

            if (recipe.Equals(Result.Error))
            {
                return Unauthorized();
            }

            return Ok(recipe);

        }

        /// <summary>
        /// Edit servings into a product that exits in a recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="productId"></param>
        /// <param name="servings"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("EditProductInRecipe", Name = "EditProductInRecipe")]
        public ActionResult<Result> EditProductInRecipe([Required] int recipeId, [Required] int productId, [Required] double servings)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipe = _repository.EditProductInRecipe(recipeId, productId, servings);

            if (recipe.Equals(Result.NotFound))
            {
                return NotFound();
            }

            if (recipe.Equals(Result.Error))
            {
                return Unauthorized();
            }

            return Ok(recipe);

        }

        /// <summary>
        /// Delete a product into a recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteProductInRecipe", Name = "DeleteProductInRecipe")]
        public ActionResult<Result> DeleteProductInRecipe([Required] int recipeId, [Required] int productId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipe = _repository.DeleteProductInRecipe(recipeId, productId);

            if (recipe.Equals(Result.Error) || recipe.Equals(Result.NotFound))
            {
                return NotFound();
            }

            return Ok(recipe);

        }

        /// <summary>
        /// Delete recipe and all its relations
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteRecipe", Name = "DeleteRecipe")]
        public ActionResult<Result> DeleteRecipe([Required] int recipeId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipe = _repository.DeleteRecipe(recipeId);

            if (recipe.Equals(Result.Error))
            {
                return NotFound();
            }

            return Ok(recipe);

        }
    }
}