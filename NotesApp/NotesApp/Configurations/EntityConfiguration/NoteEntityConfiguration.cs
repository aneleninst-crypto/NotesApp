using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesApp.Models;

namespace NotesApp.Configurations.EntityConfiguration;

public class NoteEntityConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(n => n.Id);
            
        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(n => n.Description)
            .IsRequired(false);
            
        builder.Property(n => n.Created)
            .IsRequired();
            
        builder.Property(n => n.EditDate)
            .IsRequired();
            
        builder.Property(n => n.IsCompleted)
            .IsRequired();
            
        builder.Property(n => n.Priority)
            .IsRequired();
    }
}