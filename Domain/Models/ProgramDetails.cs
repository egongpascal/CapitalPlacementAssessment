using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementAssessment.Models
{
    public class ProgramDetails
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string ProgramTitle { get; set; }
        public string ProgramSummary { get; set; }
        public string ProgramDescription { get; set; }
        public List<KeySkills> KeySkills { get; set; } = new List<KeySkills>();
        public List<ProgramBenefits>? ProgramBenefits { get; set; } = new List<ProgramBenefits>();
        public List<ApplicationCriteria> ApplicationCriterias { get; set; } = new List<ApplicationCriteria>();
        public ProgramType ProgramType { get; set; }
        public string ApplicationOpen { get; set; }
        public int Duration { get; set; }
        public Qualifications MinQualifications { get; set; }
        public string ProgramStarts { get; set; }
        public string ApplicationClose { get; set; }
        public string Location { get; set; } = "Fully Remote";
        public int MaxNumOfApplications { get; set; }
        public bool IsPublished { get; set; } = false;
    }

    public class ProgramBenefits
    {
        public int id { get; set; } 
        public string Benefit { get; set; }
    }

    public class ApplicationCriteria
    {
        public int id { get; set; } = 0;
        public string Criteria { get; set; }
    }

    public class KeySkills
    {
        public int id { get; set; } = 0;
        public string Skill { get; set; }
    }
    public enum ProgramType
    {
        PartTime,
        Fulltime
    }
    public enum Qualifications
    {
        HighSchool,
        College,
        Masters
    }
}
