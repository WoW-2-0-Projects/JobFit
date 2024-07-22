using JobFit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobFit.Persistence.EntityConfiguration;

public class RecruiterConfiguration : IEntityTypeConfiguration<Recruiter>
{
    public void Configure(EntityTypeBuilder<Recruiter> builder)
    {
        builder
            .HasMany(recruiter => recruiter.SkillSets)
            .WithOne(skill => skill.Recruiter)
            .HasForeignKey(skill => skill.RecruiterId);
    }
}