using EFCore_Prescriptions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore_Prescriptions.Configurations
{
    public class PrescriptionEntityTypeConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasData(
                new Prescription(1, DateTime.Parse("12-10-2022").Date, DateTime.Parse("12-10-2022").AddDays(7).Date, 1, 1),
                new Prescription(2, DateTime.Parse("12-10-2022").Date, DateTime.Parse("12-10-2022").AddDays(7).Date, 1, 2),
                new Prescription(3, DateTime.Parse("12-10-2022").Date, DateTime.Parse("12-10-2022").AddDays(7).Date, 2, 3)
                );
        }
    }
}
