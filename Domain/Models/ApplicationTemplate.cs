using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementAssessment.Models
{
    public class ApplicationTemplate
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? ProgramId { get; set; }
        public PersonalInfo? PersonalInfo { get; set; }
        public UserProfile? Profile { get; set; }
        public byte[]? CoverImage { get; set; }
    }

    public class PersonalInfo
    {
        public int Id { get; set; } = 0;
        public string? FirsName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        public bool IsPhoneVisible { get; set; } = false;
        public bool IsPhoneInternal { get; set; } = false;

        public bool NationalityVisible { get; set; } = false;
        public bool NationalityInternal { get; set; } = false;

        public bool LastCurrentResidenceVisible { get; set; } = false;
        public bool LastCurrentResidenceInternal { get; set; } = false;

        public bool IdNumberVisible { get; set; } = false;
        public bool IdNumberInternal { get; set; } = false;

        public bool DOBVisible { get; set; } = false;
        public bool DOBInternal { get; set; } = false;

        public bool GenderVisible { get; set; } = false;
        public bool GenderInternal { get; set; } = false;
        public List<CustomeQuestions>? CustomeQuestions { get; set; } = new List<CustomeQuestions>();
    }

    public class CustomeQuestions
    {
        public int? Id { get; set; }
        public string? Type { get; set; }
        public string? Question { get; set; }
        public string? Choice { get; set; }
    }

    public class UserProfile
    {
        public string ProgramId { get; set; }
        public bool? IsEducationVisible { get; set; } = false;
        public bool? IsEducationMandatory { get; set; } = false;
        public bool? IsResumeVisible { get; set; } = false;
        public bool? IsResumeMandatory { get; set; } = false;
        public List<Education>? Education { get; set; } = new List<Education>();
        public List<AdditionalQuestions>? AdditionalQuestions { get; set; } = new List<AdditionalQuestions>();
    }

    public class AdditionalQuestions
    {
        public int Id { set; get; } = 0;
        public string? Type { get; set; }
        public string? Question { get; set; }
        public string? Choice { get; set; }
    }

    public class Education
    {
        public int Id { get; set; } = 0;
        public string? School { get; set; }
        public DegreeType? DegreeType { get; set; } 
        public string? CourseName { get; set; }
        public string? CountryOfStudy { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? CurrentlyStudyHere { get; set; } = false;
    }
    public enum DegreeType
    {
        Bachelors,
        Masters,
        Doctorate
    }

    public class WorkExperience
    {
        public int Id { get; set; } = 0;
        public string? Company { get; set; }
        public string? JobTitle { get; set; }
        public string? JobLocation { get; set; }
        public DateOnly? StartDate { get;  set; }
        public DateOnly? EndDate { get; set; }
        public bool? ICurrentlyWorkHere { get; set; } = false;
    }
}
