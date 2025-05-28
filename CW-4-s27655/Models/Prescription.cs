using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW_4_s27655.Models;
[Table("Prescription")]
public class Prescription
{
    [Key]
    [Column("IdPrescription")]
    public int IdPrescription { get; set; }
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public DateTime DateEnd { get; set; }
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    public int PatientId { get; set; }
    public Patient Patient { get; set; }
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
    
}