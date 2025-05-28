using CW_4_s27655.Data;
using CW_4_s27655.DTOs;
using CW_4_s27655.Models;
using Microsoft.EntityFrameworkCore;

namespace CW_4_s27655.Services;

public interface IPrescriptionService
{
    Task<int> AddPrescriptionAsync(AddPrescriptionDto dto);
}
public class PrescriptionService : IPrescriptionService
{
    private readonly AppDbContext _context;

    public PrescriptionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddPrescriptionAsync(AddPrescriptionDto dto)
    {
        if (dto.Medicaments.Count == 0 || dto.Medicaments.Count > 10)
            throw new ArgumentException("Medicaments count must be between 1 and 10");
        if (dto.DateEnd < dto.Date)
        {
            throw new ArgumentException("Due date must be after date");
        }
            
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.Name == dto.Patient.FirstName &&
                                      p.Surname == dto.Patient.LastName &&
                                      p.BirthDate == dto.Patient.BirthDate);
        if (patient == null)
        {
            patient = new Patient
            {
                Name = dto.Patient.FirstName,
                Surname = dto.Patient.LastName,
                BirthDate = dto.Patient.BirthDate,
                Address = dto.Patient.Address
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        var medicamentIds = dto.Medicaments.Select(m => m.MedicamentId).ToList();
        var medicaments = await _context.Medicaments.Where(m => medicamentIds.Contains(m.IdMed)).ToListAsync();

        if (medicaments.Count != dto.Medicaments.Count)
        {
            throw new ArgumentException("Medicaments not found");
        }
        var doctor = await _context.Doctors.FindAsync(dto.DoctorId);
        if (doctor == null)
        {
            throw new ArgumentException("Doctor not found");
        }

        var prescription = new Prescription
        {
            Date = dto.Date,
            DateEnd = dto.DateEnd,
            DoctorId = dto.DoctorId,
            PatientId = patient.IdPatient,
            Description = dto.Description,
            PrescriptionMedicaments = dto.Medicaments
                .Select(m => new PrescriptionMedicament
                {
                    IdMed = m.MedicamentId,
                    Dose = m.Dose,
                    Description = m.Description,
                }).ToList()
        };
        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return prescription.IdPrescription;
    }
}