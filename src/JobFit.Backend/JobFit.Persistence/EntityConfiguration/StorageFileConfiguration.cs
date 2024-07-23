using JobFit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobFit.Persistence.EntityConfiguration;

public class StorageFileConfiguration : IEntityTypeConfiguration<StorageFile>
{
    public void Configure(EntityTypeBuilder<StorageFile> builder)
    {
        builder
            .Property(storageFile => storageFile.OriginalName)
            .HasMaxLength(128)
            .IsRequired();

        builder
            .Property(storageFile => storageFile.PhysicalName)
            .HasMaxLength(128)
            .IsRequired();

        builder
            .Property(storageFile => storageFile.ContentType)
            .HasMaxLength(128)
            .IsRequired();
        
        builder
            .Property(storageFile => storageFile.Extension)
            .HasMaxLength(128)
            .IsRequired();
        
        // builder
        //     .HasOne<User>()
        //     .WithMany()
        //     .HasForeignKey(storageFile => storageFile.OwnerId);
    }
}