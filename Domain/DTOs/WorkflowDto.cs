using CapitalPlacementAssessment.Models;

namespace CapitalPlacementAssessment.Domain.DTOs
{
    public class WorkflowDto
    {
       
        public string ProgramId { get; set; }
        public List<Stage>? Stages { get; set; } = new List<Stage>();
        
    }
}
