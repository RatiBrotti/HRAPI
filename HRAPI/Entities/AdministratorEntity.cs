using System.ComponentModel.DataAnnotations.Schema;

namespace HRAPI.Entities
{
    public class AdministratorEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual EmployeeEntity Employee { get; set; }
    }
}
