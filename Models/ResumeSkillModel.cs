namespace AI_Career_Guidence.Models
{
    public class ResumeSkillModel
    {
        public int ResumeSkillID { get; set; }
        public int ResumeID { get; set; }
        public string SkillName { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

    }
}
