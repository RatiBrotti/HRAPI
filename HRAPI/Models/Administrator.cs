namespace HRAPI.Models
{
    public class Administrator
    {
        public string IdNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string JobTitle { get; set; }
        public string Status { get; set; }
        public string Mobile { get; set; }
    }
}
