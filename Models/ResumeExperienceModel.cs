namespace AI_Career_Guidence.Models
{
    public class ResumeExperienceModel
    {
        public int ExperienceID { get; set; }
        public int ResumeID { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string WorkSummary { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

    }
}
