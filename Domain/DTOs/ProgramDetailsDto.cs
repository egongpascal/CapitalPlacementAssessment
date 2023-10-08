using CapitalPlacementAssessment.Models;
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementAssessment.Domain.DTOs
{
    public class ProgramDetailsDto
    {
        public string ProgramId { get; set; } 
        [Required(ErrorMessage = "Program Title is required.")]
        public string ProgramTitle { get; set; }
        public string ProgramSummary { get; set; }
        [Required(ErrorMessage = "Program Description is required.")]
        public string ProgramDescription { get; set; }
        public List<KeySkills> KeySkills { get; set; } 
        public List<ProgramBenefits>? ProgramBenefits { get; set; }
        public List<ApplicationCriteria> ApplicationCriterias { get; set; }

        [Required(ErrorMessage = "Program Type is required.")]
        public ProgramType ProgramType { get; set; }

        [Required(ErrorMessage = "ApplicationOpen is required.")]
        public string ApplicationOpen { get; set; }
        public int Duration { get; set; }
        public Qualifications MinQualifications { get; set; }
        public string ProgramStarts { get; set; }

        [Required(ErrorMessage = "ApplicationClose is required.")]
        public string ApplicationClose { get; set; }

        [Required(ErrorMessage = "Program Location is required.")]
        public string Location { get; set; } = "Fully Remote";
        public int MaxNumOfApplications { get; set; }
    }
}
