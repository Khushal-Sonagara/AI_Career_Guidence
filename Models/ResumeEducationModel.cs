namespace AI_Career_Guidence.Models
{
    public class ResumeEducationModel
    {
        public int EducationID { get; set; }
        public int ResumeID { get; set; }
        public string UniversityOrSchoolName { get; set; }
        public string Degree { get; set; }
        public string Major { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

    }
}
