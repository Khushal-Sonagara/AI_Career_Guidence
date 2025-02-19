namespace AI_Career_Guidence.Models
{
    public class ResumeSummaryModel
    {
        public int SummaryID { get; set; }
        public int ResumeID { get; set; }
        public string SummaryText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

    }
}
