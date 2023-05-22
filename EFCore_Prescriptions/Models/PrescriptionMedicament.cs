using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore_Prescriptions.Models
{
    [PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]
    public class PrescriptionMedicament
    {
        public int IdMedicament { get; set; }
        
        public int IdPrescription { get; set; }

        public Medicament Medicament { get; set; }

        public Prescription Prescription { get; set; }

        public int? Dose { get; set; }

        [StringLength(100)]
        public string Details { get; set; }

        public PrescriptionMedicament() { }

        public PrescriptionMedicament(int idMedicament, int idPrescription, int? dose, string details)
        {
            IdMedicament = idMedicament;
            IdPrescription = idPrescription;
            Dose = dose;
            Details = details;
        } 
        
        public PrescriptionMedicament(int idMedicament, int idPrescription, string details)
        {
            IdMedicament = idMedicament;
            IdPrescription = idPrescription;
            Details = details;
        }
    }
}
