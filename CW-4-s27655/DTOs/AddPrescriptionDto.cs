namespace CW_4_s27655.DTOs;

public class AddPrescriptionDto
{
    public PatientDto Patient { get; set; } = null!;
    public int DoctorId { get; set; }
    public List<MedicamentDto> Medicaments { get; set; } = new();
    public DateTime Date { get; set; }
    public DateTime DateEnd { get; set; }
    public string Description { get; set; } = null!;

    public int IdPatient { get; set; }
}

public class PatientDto
{
    public int PatientId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateTime BirthDate { get; set; }
}

public class MedicamentDto
{
    public int MedicamentId { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; } = null!;
}
