using CW_4_s27655.Data;
using CW_4_s27655.Models;
using CW_4_s27655.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CW_4_s27655.Services;

public interface IDoctorServices
{
    Task<Doctor?> GetDoctor(int id);
    Task<Doctor> AddDoctor(Doctor doctor);
    Task<Doctor?> UpdateDoctor(int id, AddDoctor doctor);
    Task<bool> DeleteDoctor(int id);
}

public class DoctorServices : IDoctorServices
{
    private readonly AppDbContext _dbContext;

    public DoctorServices(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Doctor?> GetDoctor(int id)
    { 
        return await _dbContext.Doctors.FindAsync(id);
    }

    public async Task<Doctor> AddDoctor(Doctor doctor)
    {
        _dbContext.Doctors.Add(doctor);
        await _dbContext.SaveChangesAsync();

        return doctor;
    }

    public async Task<Doctor?> UpdateDoctor(int id, AddDoctor doctor)
    {
        var existingDoctor = await _dbContext.Doctors.FindAsync(id);
        
        if (existingDoctor == null)
        {
            return null;
        }
        
        existingDoctor.Name = doctor.FirstName;
        existingDoctor.Surname = doctor.LastName;
        existingDoctor.email = doctor.Email;
        
        _dbContext.Doctors.Update(existingDoctor);
        await _dbContext.SaveChangesAsync();

        return existingDoctor;
    }

    public async Task<bool> DeleteDoctor(int id)
    {
        var doctor = await _dbContext.Doctors.FindAsync(id);
        
        if (doctor == null)
        {
            return false;
        }
        
        _dbContext.Remove(doctor);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }
}