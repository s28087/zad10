using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities.Configs;

namespace WebApplication1.Entities;

public class PharmacyDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }

    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }


    public PharmacyDbContext()
    {
        
    }

    public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PrescriptionEfConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DoctorEfConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PatientEfConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PrescriptionMedicamentEfConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MedicamentEfConfiguration).Assembly);
    }

}