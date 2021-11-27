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
    [Route("api/users/{userId:int}/[controller]")]
    public class PublishController : ControllerBase
    {
        private IPublicationsService _publicationsService;

        public PublishController(IPublicationsService publicationsService)
        {
            _publicationsService = publicationsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublishModel>>> GetPublish(int userId)
        {
            try
            {
                return Ok(await _publicationsService.GetPublishssAsync(userId));
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

        [HttpGet("{publishId:int}", Name = "GetPublish")]
        public async Task<ActionResult<PublishModel>> GetPublishAsync(int userId, int publishId)
        {
            try
            {
                return Ok(await _publicationsService.GetPublishAsync(userId, publishId));
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
        public async Task<ActionResult<PublishModel>> CreatePublishAsync(int userId, [FromBody] PublishModel publishModel)
        {
            try
            {
                var publishCreated = await _publicationsService.CreatePublishAsync(userId, publishModel);
                return CreatedAtRoute("GetPublish", new { userId = userId, publishId = publishCreated.Id }, publishCreated);
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

        [HttpPut("{publishId:int}")]
        public async Task<ActionResult<PublishModel>> UpdateClothesAsync(int userId, int publishId, [FromBody] PublishModel publish)
        {
            try
            {
                return Ok(await _publicationsService.UpdatePublishAsync(userId, publishId, publish));
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

        [HttpDelete("{publishId:int}")]
        public async Task<ActionResult<bool>> DeletePublishAsync(int userId, int publishId)
        {
            try
            {
                return Ok(await _publicationsService.DeletePublishAsync(userId, publishId));
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
