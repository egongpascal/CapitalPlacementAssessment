using CapitalPlacementAssessment.Domain.DTOs;
using CapitalPlacementAssessment.Repository.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreviewController : ControllerBase
    {

        private readonly IProgramRepository _progRepo;

        public PreviewController(IProgramRepository programRepository)
        {
            _progRepo = programRepository;
        }

        [HttpGet("GetPreview")]
        public async Task<IActionResult> GetPreview([FromQuery] string programId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _progRepo.GetPreview(programId);
            if (result != null)
            {
                return StatusCode(200, result);
            }
            return StatusCode(400, result);
        }
    }
}
