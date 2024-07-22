using JobFit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobFit.Persistence.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.FirstName).HasMaxLength(64).IsRequired();
        builder.Property(user => user.LastName).HasMaxLength(64).IsRequired();
        builder.Property(user => user.EmailAddress).HasMaxLength(128).IsRequired();
        builder.Property(user => user.PhoneNumber).HasMaxLength(128).IsRequired();

        builder.HasDiscriminator<string>("UserType")
            .HasValue<Employee>(nameof(Employee))
            .HasValue<Recruiter>(nameof(Recruiter));
        
    }
}