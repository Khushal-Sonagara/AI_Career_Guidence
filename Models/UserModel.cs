namespace AI_Career_Guidence.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ClerkUserId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
