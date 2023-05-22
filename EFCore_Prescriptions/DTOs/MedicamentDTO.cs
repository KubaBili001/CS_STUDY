namespace EFCore_Prescriptions.DTOs
{
    public class MedicamentDTO
    {
        public int IdMedicament { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public MedicamentDTO() { }

        public MedicamentDTO(int idMedicament, string name, string description, string type)
        {
            IdMedicament=idMedicament;
            Name=name;
            Description=description;
            Type=type;
        }
    }
}
