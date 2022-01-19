using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Softdesign.Api.Infrastructure.Interfaces;
using Softdesign.API.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Softdesign.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IApplicationService _applicationService;
        public ApplicationController(ILogger logger,
                                     IApplicationService applicationService)
        {
            _logger = logger;
            _applicationService = applicationService;
        }

        [HttpGet]
        [Route("/get")]
        public async Task<IActionResult> GetAsync()
        {
            try
             {
                _logger.LogInformation("Getting applications...");
                var response = await _applicationService.Get();
                _logger.LogInformation("Applications found!");

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Error: {e.Message}");
            }
        }

        [HttpGet]
        [Route("/get/{applicationId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int applicationId)
        {
            try
            {
                _logger.LogInformation("Getting application...");
                var response = await _applicationService.GetById(applicationId);
                _logger.LogInformation("Application found!");

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Error: {e.Message}");
            }
        }

        [HttpPost]
        [Route("/post")]
        public async Task<IActionResult> PostAsync([FromQuery, BindRequired] Application application)
        {
            try
            {
                _logger.LogInformation("Posting application...");
                var response = await _applicationService.Post(application);
                _logger.LogInformation("Application posted!");

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Error: {e.Message}");
            }
        }

        [HttpPatch]
        [Route("/path")]
        public async Task<IActionResult> PathAsync([FromQuery, BindRequired] Application application)
        {
            try
            {
                _logger.LogInformation("Patching application...");
                var response = await _applicationService.Path(application);
                _logger.LogInformation("Application patched!");

                if (!response)
                {
                    return StatusCode(204);
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Error: {e.Message}");
            }
        }

        [HttpDelete]
        [Route("/delete")]
        public async Task<IActionResult> DeleteAsync([FromQuery, BindRequired] int applicationId)
        {
            try
            {
                _logger.LogInformation("Deleting application...");
                var response = await _applicationService.Delete(applicationId);
                _logger.LogInformation("Appplication deleted!");

                if (!response)
                {
                    return StatusCode(204);
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Error: {e.Message}");
            }
        }
    }
}
