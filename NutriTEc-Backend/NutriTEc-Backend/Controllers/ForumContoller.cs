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

        /// <summary>
        /// Get comments filtered by date
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetFilteredComments", Name = "GetFilteredComments")]
        public ActionResult<List<Comment>> GetFilteredComments(int patientId, DateTime dateTime, string meal)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = _repository.GetFilteredComments(patientId, dateTime, meal);

            return Ok(comment);

        }

        /// <summary>
        /// Create comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>Result</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("CreateComment", Name = "CreateComment")]
        public ActionResult<Result> CreateComment(CommentDto comment)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.Create(comment);

            if(result == Result.Error)
            {
                Unauthorized();
            }

            return Ok();

        }
    }
}