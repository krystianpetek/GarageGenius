﻿using GarageGenius.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GarageGenius.Modules.Users.Core.Persistance.Configurations;
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(index => index.Email).IsUnique();
        builder.Property(property => property.Email).IsRequired().HasMaxLength(100).HasConversion(email => email.Value, newEmail => new ValueObjects.Email(newEmail));
        builder.Property(password => password.Password).IsRequired().HasMaxLength(100);
    }
}