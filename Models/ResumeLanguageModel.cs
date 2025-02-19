namespace AI_Career_Guidence.Models
{
    public class ResumeLanguageModel
    {
        public int LanguageID { get; set; }
        public int ResumeID { get; set; }
        public string Language { get; set; }
        public string ProficiencyLevel { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

    }
}
