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
    [Route("nutritec/admin")]
    public class AdminController : ControllerBase
    {
        private readonly INutriTEcRepository _repository;
        public AdminController(INutriTEcRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// GetAdmin
        /// </summary>
        /// <param name="admin"></param>
        /// <returns>Result</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAdmin", Name = "GetAdmin")]
        public ActionResult<List<AdminDto>> GetAdmin()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.GetAdmin();

            if(result.IsNullOrEmpty())
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        /// <summary>
        /// Enables admin to approve a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("ApproveProduct/{id}", Name = "ApproveProduct")]
        public ActionResult<Result> ApproveProduct([Required] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.ApproveProduct(id);

            if (result == Result.Error)
            {
                return Unauthorized();
            }

            if (result == Result.NotFound)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// Get payroll report for admin
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns>Pay roll report info</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetPayrollReport", Name = "GetPayrollReport")]
        public ActionResult<List<PayrollReport>> GetPayrollReport([Required] int adminId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _repository.GetPayrollReport(adminId);

            if (result.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(result);

        }
    }
}