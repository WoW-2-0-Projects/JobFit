﻿using JobFit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobFit.Persistence.EntityConfiguration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasMany(employeeDocument => employeeDocument.Documents)
            .WithOne(owner => owner.Owner)
            .HasForeignKey(owner => owner.OwnerId);
    }
}