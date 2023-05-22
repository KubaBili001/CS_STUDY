using EFCore_Prescriptions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore_Prescriptions.Configurations
{
    public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasData(
                new Patient(1, "Jakub", "Bilinski", DateTime.Parse("14-09-2001").Date),
                new Patient(2, "Adam", "Piasecki", DateTime.Parse("02-02-2002").Date),
                new Patient(3, "Jan", "Chrzanowski", DateTime.Parse("12-01-1997").Date)
            );
        }
    }
}
