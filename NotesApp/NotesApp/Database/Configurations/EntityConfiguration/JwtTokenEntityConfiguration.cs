using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApp.Models;

namespace NotesApp.Database.Configurations.EntityConfiguration;

public class JwtTokenEntityConfiguration : IEntityTypeConfiguration<JwtToken>
{
    public void Configure(EntityTypeBuilder<JwtToken> builder)
    {
        builder.HasKey(j => j.Id);
        builder.Property(j => j.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        builder.HasOne(j => j.User)
            .WithOne()
            .HasForeignKey<JwtToken>()
            .OnDelete(DeleteBehavior.Cascade);
    }
}