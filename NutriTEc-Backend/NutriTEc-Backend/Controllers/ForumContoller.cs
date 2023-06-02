using Microsoft.AspNetCore.Mvc;
using Nest;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Entities;
using NutriTEc_Backend.Helpers;
using NutriTEc_Backend.Repository.Interface;
using System.ComponentModel.DataAnnotations;

namespace NutriTEc_Backend.Controllers
{
    [ApiController]
    [Route("nutritec/forum")]
    public class ForumController : ControllerBase
    {
        private readonly IForumRepository _repository;
        public ForumController(IForumRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all comments
        /// </summary>
        /// <returns>Comment</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetComments", Name = "GetComments")]
        public ActionResult<Comment> GetComments()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = _repository.GetAll();

            return Ok(comment);
            
        }
    }
}