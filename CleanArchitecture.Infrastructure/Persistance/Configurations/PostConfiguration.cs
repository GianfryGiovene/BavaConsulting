using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistance.Configurations;

public sealed class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(
            postId => postId.Id,
            value => new PostId(value));

        builder.Property(p => p.Title)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Content)
            .HasColumnType("varchar(max)")
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(p => p.Like)
            .HasColumnType("int");

        builder.Property(p => p.Unlike)
            .HasColumnType("int");

        builder.HasOne<User>()
            .WithMany(p => p.Posts)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasMany<Category>(s => s.Categories)
                .WithMany(c => c.Posts);
    }
}
