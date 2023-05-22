namespace EFCore_Prescriptions.DTOs
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public DoctorDTO() { }

        public DoctorDTO(int id, string name, string lastName, string email)
        {
            Id = id;
            Name=name;
            LastName=lastName;
            Email=email;
        }
    }
}
