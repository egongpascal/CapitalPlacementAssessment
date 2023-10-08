namespace CapitalPlacementAssessment.Models
{
    public class WorkFlow
    {
        public List<Stage> Stages { get; set; } = new List<Stage>();
    }
    public class Stage
    {
        public string Id { get; set; }
        public string StageName { get; set; }
        public List<StageType> StageTypes { get; set; } = new List<StageType>();
        
    }

    public class StageType
    {
        public Shortlisting? shortlisting { get; set; }
        public VideoInterview? videoInterview { get; set; }
    }
    public class Shortlisting
    {

    }
    public class VideoInterview
    {
        public string InterviewQuestion { get; set; }
        public string AdditionalInformation { get; set; }
        public int VideoDuration { get; set; }
        public DateOnly SubmissionDeadline { get; set; }
    }
}
