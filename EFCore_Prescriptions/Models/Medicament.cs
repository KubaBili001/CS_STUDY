using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore_Prescriptions.Models
{
    public class Medicament
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMedicament { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

        public Medicament() { }

        public Medicament(int id, string name, string desc, string type)
        {
            IdMedicament = id;
            Name = name;
            Description = desc; 
            Type = type;
        }
    }
}
