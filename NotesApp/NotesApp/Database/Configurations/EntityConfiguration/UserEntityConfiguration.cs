using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApp.Models;

namespace NotesApp.Database.Configurations.EntityConfiguration;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
            
        builder.Property(u => u.Login)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(u => u.Password)
            .IsRequired();

        builder.HasMany(u => u.Notes)
            .WithOne(n => n.User)
            .OnDelete(DeleteBehavior.Cascade);
    }
}