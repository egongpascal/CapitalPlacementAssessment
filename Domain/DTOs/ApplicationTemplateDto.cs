using CapitalPlacementAssessment.Models;

namespace CapitalPlacementAssessment.Domain.DTOs
{
    public class ApplicationTemplateDto
    {      
        public string ProgramId { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
        public UserProfile Profile { get; set; }
        public byte[] CoverImage { get; set; }       
    }
}
