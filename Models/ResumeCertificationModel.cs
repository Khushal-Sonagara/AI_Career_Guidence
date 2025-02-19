namespace AI_Career_Guidence.Models
{
    public class ResumeCertificationModel
    {
        public int CertificationID { get; set; }
        public int ResumeID { get; set; }
        public string CertificationName { get; set; }
        public string IssuingOrganization { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string CredentialID { get; set; }
        public string CredentialURL { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

    }
}
