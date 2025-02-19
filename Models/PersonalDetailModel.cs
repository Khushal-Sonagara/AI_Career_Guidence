namespace AI_Career_Guidence.Models
{
    public class PersonalDetailModel
    {
            public int PersonalDetailID { get; set; }
            public int ResumeID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string JobTitle { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string LinkedIn { get; set; }
            public DateTime? CreatedAt { get; set; }  // Nullable DateTime
            public DateTime? UpdatedAt { get; set; }  // Nullable DateTime
    }
}
