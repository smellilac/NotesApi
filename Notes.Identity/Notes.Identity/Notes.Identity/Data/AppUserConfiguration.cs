﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Identity.Models;

namespace Notes.Identity.Data;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(appUser => appUser.Id);

        builder.Property(appUser => appUser.FirstName)
               .IsRequired(false); 

        builder.Property(appUser => appUser.LastName)
               .IsRequired(false);
    }
}
