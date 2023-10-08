using CapitalPlacementAssessment.Domain.DTOs;
using CapitalPlacementAssessment.Repository.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProgramsController : ControllerBase
    {
        private readonly IProgramRepository _programRepo;

        public ProgramsController(IProgramRepository programRepo)
        {
            _programRepo = programRepo;
        }

        [HttpPost("CreateProgram")]
        public async Task<IActionResult> CreateProgram([FromBody] ProgramDetailsDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _programRepo.CreateProgram(request);
            if(result != null)
            {
                return StatusCode(200, result);
            }
            return StatusCode(400, result);
        }

        [HttpPut("UpdateProgram")]
        public async Task<IActionResult> UpdateProgram([FromBody] ProgramDetailsDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _programRepo.UpdateProgram(request);
            if (result != null)
            {
                return StatusCode(200, result);
            }
            return StatusCode(400, result);
        }

        [HttpGet("GetProgram")]
        public async Task<IActionResult> GetProgram([FromQuery] string programId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _programRepo.GetProgram(programId);
            if (result != null)
            {
                return StatusCode(200, result);
            }
            return StatusCode(400, result);
        }
    }
}
