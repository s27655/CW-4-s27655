using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW_4_s27655.Models;
[Table("Patient")]
public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}