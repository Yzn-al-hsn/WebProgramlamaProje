using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProgramalamaProje.Models;

namespace WebProgramalamaProje.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<TownModel> Towns { get; set; }
        public DbSet<HospitalModel> Hospital { get; set; }
        public DbSet<ClinicModel> Clinics { get; set; }
        public DbSet<ClinicWorkTimesModel> ClinicWorkTimes { get; set; }
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<DoctorWorkTimesModel> DoctorWorkTimes { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }

    }
}
