using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore_Prescriptions.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPatient { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; }

        public Patient() { }

        public Patient(int id, string name, string lastName, DateTime birthdate)
        {
            IdPatient = id;
            FirstName = name;
            LastName = lastName;
            Birthdate = birthdate;
        }
    }
}
