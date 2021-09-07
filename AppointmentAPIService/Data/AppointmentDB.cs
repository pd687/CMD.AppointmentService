using CMD.Appointment.Domain.Entities;
using System.Data.Entity;

namespace Data
{
    public class AppointmentDB : DbContext
    {
        // Your context has been configured to use a 'AppointmentDB' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Data.AppointmentDB' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AppointmentDB' 
        // connection string in the application configuration file.
        public AppointmentDB()
            : base("name=AppointmentDB")
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }

        public virtual DbSet<Doctor> Doctors { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}