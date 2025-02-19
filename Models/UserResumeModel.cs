using DocumentFormat.OpenXml.Spreadsheet;

namespace AI_Career_Guidence.Models
{
    public class UserResumeModel
    {
        public int ResumeID { get; set; }
        public int UserID { get; set; }
        public string ResumeTitle { get; set; }
        public int ResumeImageID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        //public UserModel User { get; set; }
        //public ICollection<PersonalDetailModel> PersonalDetails { get; set; }
        //public ICollection<ResumeSummaryModel> ResumeSummaries { get; set; }
        //public ICollection<ResumeExperienceModel> ResumeExperiences { get; set; }
        //public ICollection<ResumeEducationModel> ResumeEducations { get; set; }
        //public ICollection<ResumeSkillModel> ResumeSkills { get; set; }
        //public ICollection<ResumeProjectModel> ResumeProjects { get; set; }
        //public ICollection<ResumeCertificationModel> ResumeCertifications { get; set; }
        //public ICollection<ResumeLanguageModel> ResumeLanguages { get; set; }
        //public ICollection<ResumeInterestModel> ResumeInterests { get; set; }
    }
}
