using EFCore_Prescriptions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore_Prescriptions.Configurations
{
    public class MedicamentEntityTypeConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.HasData(
                new Medicament(1, "Zortax", "desc1", "type1"),    
                new Medicament(2, "Chloroform", "desc2", "type2"),    
                new Medicament(3, "Battery Acid", "desc4", "type3")    
            );
        }
    }
}
