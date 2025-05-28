namespace CW_4_s27655.DTOs;

public class PatientDetailsDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string Address { get; set; } = null!;
    public List<PrescriptionDto> Prescriptions { get; set; } = new();
}

public class PrescriptionDto
{
    public int PrescriptionId { get; set; }
    public DateTime Date { get; set; }
    public DateTime DateEnd { get; set; }
    public string Description { get; set; } = null!;
    public List<MedicamentInPrescriptionDto> Medicaments { get; set; } = new();
    public DoctorDto Doctor { get; set; } = null!;
}

public class MedicamentInPrescriptionDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; } = null!;
    public int Dose { get; set; }
    public string Description { get; set; } = null!;
}

public class DoctorDto
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
}
