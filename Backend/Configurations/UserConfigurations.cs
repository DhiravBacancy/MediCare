using MediCare.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Primary key
        builder.HasKey(u => u.UserId);

        // Basic user information
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        // Add unique constraint to Email if needed
        builder.HasIndex(u => u.Email)
            .IsUnique();  // Ensures email is unique across users

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.MobileNo)
            .HasMaxLength(15);

        builder.Property(u => u.EmergencyNo)
            .HasMaxLength(15); // EmergencyNo can have similar length as MobileNo

        // User credentials and role information
        builder.Property(u => u.RoleId)
            .IsRequired();  // Ensure RoleId is required

        // Foreign key relationship with Role
        builder.HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete (optional)

        // Status and activity
        builder.Property(u => u.Active)
            .IsRequired();

        // Timestamps for record tracking
        builder.Property(u => u.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);  // Typically store email or username for CreatedBy

        builder.Property(u => u.CreatedAt)
            .IsRequired();  // CreatedAt should always be required

        builder.Property(u => u.UpdatedBy)
            .HasMaxLength(100);  // UpdatedBy can be nullable, but still restricting length

        builder.Property(u => u.UpdatedAt)
            .IsRequired(false);  // Nullable, as a user may not have been updated yet

        // New properties for RefreshToken functionality
        builder.Property(u => u.RefreshToken)
            .HasMaxLength(500);  // Adjust max length based on your expected token size

    }
}
