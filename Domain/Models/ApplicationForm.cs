namespace CapitalPlacementAssessment.Models
{
    public class ApplicationForm
    {
        public string Id { get; set; }
        public string ProgramId { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
        public Profile Profile { get; set; }
    }

    public class PersonalInfo
    {
        public int Id { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public bool IsPhoneVisible { get; set; }
        public bool IsPhoneInternal { get; set; }

        public bool NationalityVisible { get; set; }
        public bool NationalityInternal { get; set; }

        public bool LastCurrentResidenceVisible { get; set; }
        public bool LastCurrentResidenceInternal { get; set; }

        public bool IdNumberVisible { get; set; }
        public bool IdNumberInternal { get; set; }

        public bool DOBVisible { get; set; }
        public bool DOBInternal { get; set; }

        public bool GenderVisible { get; set; }
        public bool GenderInternal { get; set; }
        public List<CustomeQuestions>? CustomeQuestions { get; set; }
    }

    public class CustomeQuestions
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        public string? Choice { get; set; }
    }

    public class Profile
    {
        public string ProgramId { get; set; }
        public bool IsEducationVisible { get; set; }
        public bool IsEducationMandatory { get; set; }
        public bool IsResumeVisible { get; set; }
        public bool IsResumeMandatory { get; set; }
        public List<Education> Education { get; set; }
        public List<AdditionalQuestions>? AdditionalQuestions { get; set; }
    }

    public class AdditionalQuestions
    {
        public int Id { set; get; }
        public string Type { get; set; }
        public string Question { get; set; }
        public string? Choice { get; set; }
    }

    public class Education
    {
        public int Id { get; set; }
        public string School { get; set; }
        public DegreeType DegreeType { get; set; } 
        public string CourseName { get; set; }
        public string CountryOfStudy { get; set; }
        public DateOnly StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? CurrentlyStudyHere { get; set; }
    }
    public enum DegreeType
    {
        Bachelors,
        Masters,
        Doctorate
    }

    public class WorkExperience
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string JobTitle { get; set; }
        public string JobLocation { get; set; }
        public DateOnly StartDate { get;  set; }
        public DateOnly? EndDate { get; set; }
        public bool ICurrentlyWorkHere { get; set; }
    }
}
