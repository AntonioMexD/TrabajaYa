using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabajaYa.Models;
using TrabajaYaAPI.Exceptions;
using TrabajaYaAPI.Services;

namespace TrabajaYaAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUsersService _userService;
        public UsersController(IUsersService userService)
        {
            this._userService = userService;
        }

        //api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsersAsync(string orderBy = "Id", bool showPublish = false)
        {
            try
            {
                return Ok(await _userService.GetUsersAsync(orderBy, showPublish));
            }
            catch (BadRequestOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }

        //api/boutiques/boutiqueId
        [HttpGet("{userId:int}", Name = "GetUser")]
        public async Task<ActionResult<UserModel>> GetUserAsync(int userId, bool showPublish = false)
        {
            try
            {
                return await _userService.GetUserAsync(userId, showPublish);
            }
            catch (NotFoundOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateUserAsync([FromBody] UserModel userModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var url = HttpContext.Request.Host;
                var newUser = await _userService.CreateUserAsync(userModel);
                return CreatedAtRoute("GetUser", new { userId = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }

        [HttpDelete("{userId:int}")]
        public async Task<ActionResult<DeleteModel>> DeleteUserAsync(int userId)
        {
            try
            {
                return Ok(await _userService.DeleteUserAsync(userId));
            }
            catch (NotFoundOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }

        [HttpPut("{userId:int}")]
        public async Task<IActionResult> UpdateUserAsync(int userId, [FromBody] UserModel userModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    foreach (var pair in ModelState)
                    {
                        if (pair.Key == nameof(userModel.Name) && pair.Value.Errors.Count > 0)
                        {
                            return BadRequest(pair.Value.Errors);
                        }
                    }
                }
                return Ok(await _userService.UpdateUserAsync(userId, userModel));
            }
            catch (NotFoundOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }

    }
}
