namespace AI_Career_Guidence.Models
{
    public class ResumeInterestModel
    {
        public int InterestID { get; set; }
        public int ResumeID { get; set; }
        public string Interest { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

    }
}
