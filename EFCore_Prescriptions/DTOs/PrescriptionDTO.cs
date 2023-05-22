namespace EFCore_Prescriptions.DTOs
{
    public class PrescriptionDTO
    {

        public int IdPrescription { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public PatientDTO Patient { get; set; }

        public DoctorDTO Doctor { get; set; }

        public List<MedicamentDTO> Medicaments { get; set; }

        public PrescriptionDTO() { }

        public PrescriptionDTO(int id, DateTime date, DateTime due, PatientDTO patient, DoctorDTO doctor, List<MedicamentDTO> medicaments)
        {
            IdPrescription = id;
            Date = date;
            DueDate = due;
            Patient = patient;
            Doctor = doctor;
            Medicaments = medicaments;
        }

    }
}
