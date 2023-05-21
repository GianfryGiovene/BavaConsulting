using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistance.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            postId => postId.Id,
            value => new CategoryId(value));

        builder.Property(c => c.Name)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(c => c.Description)
           .HasColumnType("varchar(255)")
           .HasMaxLength(255);   
            

    }
}
