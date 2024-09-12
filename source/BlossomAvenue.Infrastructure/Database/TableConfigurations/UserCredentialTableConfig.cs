using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlossomAvenue.Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlossomAvenue.Infrastructure.Database.TableConfigurations
{
    public class UserCredentialTableConfig : IEntityTypeConfiguration<UserCredential>
    {
        public void Configure(EntityTypeBuilder<UserCredential> b)
        {
            b.Property<Guid>("UserId")
                .HasColumnType("uuid")
                .HasColumnName("user_id");

            b.Property<string>("Password")
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("password");

            b.Property<byte[]>("Salt")
                .IsRequired()
                .HasColumnType("bytea")
                .HasColumnName("salt");

            b.Property<string>("UserName")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(250)
                .HasColumnName("user_name");

            b.HasKey("UserId")
                .HasName("user_credentials_pk");

            b.ToTable("user_credentials", (string)null);

            b.HasData(SeedDataUsers.UserCredentials);


        }

    }
}