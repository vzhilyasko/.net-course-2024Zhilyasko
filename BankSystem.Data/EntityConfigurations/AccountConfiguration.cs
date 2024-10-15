using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityConfigurations
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("account");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("uuid");

            builder.Property(c => c.Amount)
                .HasColumnType("decimal")
                .IsRequired();

            builder.Property(c => c.Currency)
                .HasColumnType("varchar")
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(c => c.ClientId)
                .HasColumnType("uuid")
                .IsRequired();

            builder.HasKey(e => e.Id)
                .HasName("id");

            builder.HasOne(x => x.Client)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
