namespace CapitalPlacementAssessment.Models
{
    public class ProgramDetails
    {
        public int ProgramId { get; set; }
        public string ProgramTitle { get; set; }
        public string ProgramSummary { get; set; }
        public string ProgramDescription { get; set; }
        public string? KeySkills { get; set; }
        public string? ProgramBenefits { get; set; }
        public string? ApplicationCriteria { get; set; }
        public ProgramType ProgramType { get; set; }
        public DateOnly ApplicationOpen { get; set; }
        public int Duration { get; set; }
        public Qualifications MinQualifications { get; set; }
        public DateOnly ProgramStarts { get; set; }
        public DateOnly ApplicationClose { get; set; }
        public string Location { get; set; } = "Fully Remote";
        public int MaxNumOfApplications { get; set; }
        public bool IsPublished { get; set; } = false;
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
