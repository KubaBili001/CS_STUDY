using EFCore_Prescriptions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore_Prescriptions.Configurations
{
    public class DoctorEntityTypeConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasData(
                new Doctor(1, "Ein", "Zwein", "12@gmail.com"),
                new Doctor(2, "Drei", "Vier", "34@gmail.com"),
                new Doctor(3, "Funf", "Sechs", "56@gmail.com")
            );
        }
    }
}
