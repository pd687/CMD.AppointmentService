using System;
using System.ComponentModel.DataAnnotations;

namespace CMD.Appointment.Domain.Entities
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime AppointmentTime { get; set; }
        public string Status { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

    }
}
