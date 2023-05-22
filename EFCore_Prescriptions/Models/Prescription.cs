using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore_Prescriptions.Models
{
    public class Prescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPrescription { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        [ForeignKey("PatientInstance")]
        public int IdPatient { get; set; }
        
        [ForeignKey("DoctorInstance")]
        public int IdDoctor { get; set; }

        public Patient PatientInstance { get; set; }

        public Doctor DoctorInstance { get; set; }

        public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

        public Prescription() { }

        public Prescription(int id, DateTime now, DateTime due, int idPatient, int idDoctor)
        {
            IdPrescription = id;
            Date = now;
            DueDate = due;
            IdPatient = idPatient;
            IdDoctor = idDoctor;
        }

    }
}
