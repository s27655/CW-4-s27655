using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CW_4_s27655.Models;
[Table("Medicament")]
public class Medicament
{
    [Key]
    public int IdMed { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}