namespace EFCore_Prescriptions.DTOs
{
    public class PatientDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        public PatientDTO() { }

        public PatientDTO(int id, string name, string lastName, DateTime birthdate)
        {
            Id = id;
            Name=name;
            LastName=lastName;
            Birthdate=birthdate;
        }
    }
}
