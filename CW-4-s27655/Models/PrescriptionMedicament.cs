using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CW_4_s27655.Models;

[Table("PrescriptionMedicament")]
[PrimaryKey(nameof(IdPrescription), nameof(IdMed))]
public class PrescriptionMedicament
{
    [Column("IdPrescription")]
    public int IdPrescription { get; set; }
    public Prescription Prescription { get; set; }
    [Column("IdMed")]
    public int IdMed { get; set; }
    public Medicament Medicament { get; set; }
    
    public int Dose { get; set; }
    public string Description { get; set; }
    
    
}