using CapitalPlacementAssessment.Domain.DTOs;
using CapitalPlacementAssessment.Repository.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationTemplatesController : ControllerBase
    {
        private readonly IApplicationTemplateRepo _appTempRepo;

        public ApplicationTemplatesController(IApplicationTemplateRepo templateRepo)
        {
            _appTempRepo = templateRepo;
        }


        [HttpPut("UpdateApplicationTemplate")]
        public async Task<IActionResult> UpdateApplicationTemplate([FromBody] ApplicationTemplateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _appTempRepo.UpdateApplicationTemplate(request);
            if (result != null)
            {
                return StatusCode(200, result);
            }
            return StatusCode(400, result);
        }

        [HttpGet("GetApplicationTemplate")]
        public async Task<IActionResult> GetApplicationTemplate([FromQuery] string programId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _appTempRepo.GetApplicationTemplate(programId);
            if (result != null)
            {
                return StatusCode(200, result);
            }
            return StatusCode(400, result);
        }
    }
}
