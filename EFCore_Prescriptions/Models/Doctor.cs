using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore_Prescriptions.Models
{
    public class Doctor
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDoctor { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; }

        public Doctor() { }

        public Doctor(int id, string name, string lastName, string email)
        {
            IdDoctor = id;
            FirstName = name;
            LastName = lastName;
            Email = email;
        }
    }
}
