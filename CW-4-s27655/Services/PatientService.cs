using CW_4_s27655.DTOs;

namespace CW_4_s27655.Services;

using Microsoft.EntityFrameworkCore;
using CW_4_s27655.Data;
using CW_4_s27655.DTOs;
public interface IPatientService
{
    Task<PatientDetailsDto?> GetPatientDetailsAsync(int id);
}


public class PatientService : IPatientService
{
    private readonly AppDbContext _context;

    public PatientService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PatientDetailsDto?> GetPatientDetailsAsync(int id)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.PrescriptionMedicaments)
                    .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Doctor)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        if (patient == null)
            return null;

        var result = new PatientDetailsDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.Name,
            LastName = patient.Surname,
            BirthDate = patient.BirthDate,
            Address = patient.Address,
            Prescriptions = patient.Prescriptions
                .OrderBy(pr => pr.DateEnd)
                .Select(pr => new PrescriptionDto
                {
                    PrescriptionId = pr.IdPrescription,
                    Date = pr.Date,
                    DateEnd = pr.DateEnd,
                    Description = pr.Description,
                    Medicaments = pr.PrescriptionMedicaments.Select(pm => new MedicamentInPrescriptionDto
                    {
                        IdMedicament = pm.IdMed,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose,
                        Description = pm.Description
                    }).ToList(),
                    Doctor = new DoctorDto
                    {
                        IdDoctor = pr.Doctor.Id,
                        FirstName = pr.Doctor.Name,
                        LastName = pr.Doctor.Surname,
                        Email = pr.Doctor.email
                    }
                }).ToList()
        };

        return result;
    }
}
