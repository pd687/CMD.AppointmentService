using System;

namespace CMD.Appointment.Domain.ApiModels
{
    public class AppointmentAPIModel
    {

        public int Id { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string Status { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

    }
}
