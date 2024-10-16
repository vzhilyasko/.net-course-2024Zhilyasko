using BankSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityConfigurations
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("client");

            builder.Property(e => e.Id)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(c => c.FirstName)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasColumnName("first_name")
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasColumnName("last_name")
                .IsRequired();

            builder.Property(c => c.MidlleName)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasColumnName("midlle_name");

            builder.Property(c => c.Birthday)
                .HasColumnType("date")
                .HasColumnName("birthday")
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasColumnName("e_mail")
                .IsRequired();

            builder.Property(c => c.PhoneNumber)
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .HasColumnName("phone_name")
                .IsRequired();

            builder.Property(c => c.PassportSeriya)
                .HasColumnType("varchar")
                .HasMaxLength(4)
                .HasColumnName("passport_seriya")
                .IsRequired();

            builder.Property(c => c.PassportNumber)
                .HasColumnType("varchar")
                .HasMaxLength(7)
                .HasColumnName("passport_number")
                .IsRequired();

            builder.HasKey(e => e.Id);
            
        }
    }
}
