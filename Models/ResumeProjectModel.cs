namespace AI_Career_Guidence.Models
{
    public class ResumeProjectModel
    {
        public int ProjectID { get; set; }
        public int ResumeID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TechnologiesUsed { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ProjectURL { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

    }
}
