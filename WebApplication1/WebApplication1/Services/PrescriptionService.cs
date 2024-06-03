using WebApplication1.Entities;

namespace WebApplication1.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly PharmacyDbContext _context;

    public PrescriptionService(PharmacyDbContext context)
    {
        _context = context;
    }

    //Jeśli pacjent przekazany w żądaniu nie istnieje, wstawiamy nowego pacjenta do tabeli Pacjent
    public async Task<Patient> CreatePatient(Patient patient)
    {
        var exPatient = _context.Patients.FirstOrDefault(p => p.IdPatient == patient.IdPatient);
        if (exPatient != null)
        {
            return exPatient;
        }
        
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
        return patient;
    }
    
    public async Task<Doctor> GetDoctor(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null)
        {
            throw new InvalidOperationException("brak doktora");
        }
        
        return doctor;
    }

    //Jeśli lek podany na recepcie nie istnieje, zwracamy błąd.
    public async Task<bool> IfMedicamentExist(IEnumerable<int> MedsId)
    {
        foreach (var MedtId in MedsId)
        {
            var existingMedicament = await _context.Medicaments.FindAsync(MedtId);
            if (existingMedicament == null)
            {
                return false;
            }
        }
        return true;
    }
    
    //Recepta może obejmować maksymalnie 10 leków. W innym wypadku zwracamy błąd.
    //Musimy sprawdzić czy DueData>=Date

    public void CheckPrescription(Prescription prescription)
    {
        if (prescription.PrescriptionMedicaments.Count > 10)
        {
            throw new Exception("Recepta może obejmować maksymalnie 10 leków");
        }
        
        if (prescription.DueDate < prescription.Date)
        {
            
            throw new InvalidOperationException("DueDate musi byc wieksze od Date");
        }
            
    }
    public async Task<Prescription> AddPrescription(Prescription prescription)
    {
        CheckPrescription(prescription);

        var patient = await CreatePatient(prescription.Patient);
        var exMed = await IfMedicamentExist
            (prescription.PrescriptionMedicaments.Select(p => p.IdMedicament));
        
        if (!exMed)
        {
            throw new InvalidOperationException("leki nie istnieją.");
        }

        var doctor = await GetDoctor(prescription.IdDoctor);

        prescription.Patient = patient;
        prescription.Doctor = doctor;
        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
        return prescription;
    }
}