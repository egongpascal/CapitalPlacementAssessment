using CapitalPlacementAssessment.Models;

namespace CapitalPlacementAssessment.Domain.DTOs
{
    public class PreviewDto
    {
        public string ProgramSummary { get; set; }
        public string ProgramDescription { get; set; }
        public List<KeySkills> KeySkills { get; set; } 
        public List<ProgramBenefits> ProgramBenefits { get; set; }
        public List<ApplicationCriteria> ApplicationCriterias { get; set; }
    }
}