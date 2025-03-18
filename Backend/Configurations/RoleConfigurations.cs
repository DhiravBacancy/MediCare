using MediCare.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediCare.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.RoleId);  // Define primary key

            // Define properties
            builder.Property(r => r.RoleName)
                .IsRequired()  // RoleName is required
                .HasMaxLength(50);  // Max length for RoleName

        }
    }
}
