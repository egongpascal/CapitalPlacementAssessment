using CapitalPlacementAssessment.Domain.DTOs;
using CapitalPlacementAssessment.Repository.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowsController : ControllerBase
    {

        private readonly IWorkflowRepository _workflowRepo;

        public WorkflowsController(IWorkflowRepository workflowRepo)
        {
            _workflowRepo = workflowRepo;
        }


        [HttpPut("UpdateWorkflow")]
        public async Task<IActionResult> UpdateWorkFlow([FromBody] WorkflowDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _workflowRepo.UpdateWorkFlow(request);
            if (result != null)
            {
                return StatusCode(200, result);
            }
            return StatusCode(400, result);
        }

        [HttpGet("GetWorkflow")]
        public async Task<IActionResult> GetWorkflow([FromBody] string programId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _workflowRepo.GetWorkFlow(programId);
            if (result != null)
            {
                return StatusCode(200, result);
            }
            return StatusCode(400, result);
        }
    }
}
