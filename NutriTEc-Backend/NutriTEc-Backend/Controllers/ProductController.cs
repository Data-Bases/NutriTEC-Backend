using Microsoft.AspNetCore.Mvc;
using Nest;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Repository.Interface;
using System.ComponentModel.DataAnnotations;

namespace NutriTEc_Backend.Controllers
{
    [ApiController]
    [Route("nutritec/product")]
    public class ProductController : ControllerBase
    {
        private readonly INutriTEcRepository _repository;
        public ProductController(INutriTEcRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get All Products
        /// </summary>
        /// <returns>
        /// Products name
        /// Products Id
        /// </returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAllProducts", Name = "GetAllProducts")]
        public ActionResult<ProductDto> GetAllProducts()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = _repository.GetAllProducts();

            return Ok(products);
        }

        /// <summary>
        /// Get a Product by its Barcode
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns>ProductInformationDto</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetProductById/{id}", Name = "GetProductById/{id}")]
        public ActionResult<ProductInformationDto> GetProductById([Required] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _repository.GetProductById(id);

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        /// <summary>
        /// Get a Product by its Barcode and its servings
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns>ProductInformationDto</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetProductByIdAndServings", Name = "GetProductByIdAndServings")]
        public ActionResult<ProductInformationDto> GetProductByIdAndServings([Required] int id, [Required] double servings)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _repository.GetProductByIdAndServings(id, servings);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Create a New Product Not Approved
        /// </summary>
        /// <param name="productInformationDto"></param>
        /// <returns>Result</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("AddNewProduct", Name = "AddNewProduct")]
        public ActionResult<Result> AddNewProduct([Required] ProductInformationDto productInformationDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.AddNewProduct(productInformationDto);

            if (result == Result.Error)
            {
                return Unauthorized();
            }

            return Ok();

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetUnapprovedProducts", Name = "GetUnapprovedProducts")]
        public ActionResult<ProductDto> GetUnapprovedProducts()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = _repository.GetUnapprovedProducts();

            return Ok(products);
        }
    }
}
