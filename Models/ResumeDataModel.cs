namespace AI_Career_Guidence.Models
{
    public class ResumeDataModel
    {
        //public int ResumeID { get; set; }
        //public int UserID { get; set; }
        //public string ResumeTitle { get; set; }
        //public DateTime ResumeCreatedAt { get; set; }
        //public DateTime? ResumeUpdatedAt { get; set; }
        public UserResumeModel Resume {  get; set; }
        public PersonalDetailModel PersonalDetails { get; set; }
        public ResumeSummaryModel ResumeSummary { get; set; }
        public List<ResumeExperienceModel> ResumeExperiences { get; set; }
        public List<ResumeEducationModel> ResumeEducation { get; set; }
        public List<ResumeSkillModel> ResumeSkills { get; set; }
        public List<ResumeProjectModel> ResumeProjects { get; set; }
        public List<ResumeCertificationModel> ResumeCertifications { get; set; }
        public List<ResumeLanguageModel> ResumeLanguages { get; set; }
        public List<ResumeInterestModel> ResumeInterests { get; set; }
        public ResumePhotoModel ResumePhoto { get; set; }
    }
}
