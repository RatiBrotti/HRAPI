namespace HRAPI.Entities
{
    public class EmployeeEntity
    {
        public int Id { get; set; }
        public string IdNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string JobTitle { get; set; }
        public string Status { get; set; }
        public DateTime? DismissalDate { get; set; }
        public string Mobile { get; set; }
    }
}
