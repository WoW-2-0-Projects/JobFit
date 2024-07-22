using JobFit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobFit.Persistence.EntityConfiguration;

public class SkillSetConfiguration :IEntityTypeConfiguration<SkillSet>
{
    public void Configure(EntityTypeBuilder<SkillSet> builder)
    {
        builder.Property(skill => skill.Languages).IsRequired();
        builder.Property(skill => skill.Education).IsRequired();
        builder.Property(skill => skill.WorkExperiences).IsRequired();
        builder.Property(skill => skill.Skills).IsRequired();
    }
}