using EFCore_Prescriptions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore_Prescriptions.Configurations
{
    public class PrescriptionMedicamentEntityTypeConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
    {
        public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
        {
            builder.HasData(
                new PrescriptionMedicament(1, 1, 10, "Some Details"),
                new PrescriptionMedicament(2, 2, "Some Details"),
                new PrescriptionMedicament(1, 3, 10, "Some Details 3")
            );
        }
    }
}
