using WebApplication1.Entities;

namespace WebApplication1.Services;

public interface IPrescriptionService
{
    /*Task<Patient> CreatePatient(Patient patient);
    Task<Doctor> GetDoctor(int id);
    Task<bool> IfMedicamentExist(IEnumerable<int> MedsId);
    void CheckPrescription(Prescription prescription);*/
    Task<Prescription> AddPrescription(Prescription prescription);
    

}