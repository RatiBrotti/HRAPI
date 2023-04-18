using System.ComponentModel.DataAnnotations.Schema;

namespace HRAPI.Entities
{
    public class Administrator
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Employee Employee { get; set; }
    }
}
