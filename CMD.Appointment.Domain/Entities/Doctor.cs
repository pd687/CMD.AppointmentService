using System.ComponentModel.DataAnnotations;

namespace CMD.Appointment.Domain.Entities
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
