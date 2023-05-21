using CleanArchitecture.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistance.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users")
            .HasKey(t => t.Id)
            .IsClustered();

        builder.Property(u => u.Id).HasConversion(
            userId => userId.Value,
            value => new UserId(value));

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(t => t.Email)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.FiscalCode)
            .HasColumnName("fiscal_code")
            .HasColumnType("varchar(20)")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(u => u.FirstName)
            .HasColumnName("first_name")
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .HasColumnName("last_name")
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(u => u.BirthDay)
            .HasColumnName("birth_day")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(u => u.PasswordSalt)
            .HasColumnName("password_salt")
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .IsRequired();

        builder.Property(u=>u.CreatedAd)
            .HasColumnName("created_at")
            .HasColumnType("datetime")
            .IsRequired();
    }
}
