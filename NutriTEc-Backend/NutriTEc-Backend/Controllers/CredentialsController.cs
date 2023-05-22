using Microsoft.AspNetCore.Mvc;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Helpers;
using NutriTEc_Backend.Repository.Interface;
using System.ComponentModel.DataAnnotations;

namespace NutriTEc_Backend.Controllers
{
    [ApiController]
    [Route("nutritec/credentials")]
    public class CredentialsController : ControllerBase
    {
        private readonly INutriTEcRepository _repository;

        public CredentialsController(INutriTEcRepository repository)
        {
           _repository = repository;
        }

        /// <summary>
        /// Log in to all users
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("LogIn", Name = "LogIn")]
        public ActionResult<UserInfoDto> LogIn([Required] string email, [Required] string password)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _repository.GetUserByEmail(email);

            if (string.IsNullOrEmpty(user.Password))
            {
                return NotFound();
            }

            var expectedEncodedPassword = PassowordHelper.EncodePasswordMD5(password).ToLower();

            if (!expectedEncodedPassword.Equals(user.Password))
            {
                return Unauthorized();
            }

            var userInfo = new UserInfoDto
            {
                Id = user.Id,
                Name = user.Name,
                UserType = user.UserType
            };

            return Ok(userInfo);
        }

    }
}