using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistance.Configurations;

public sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            postId => postId.Id,
            value => new CommentId(value));

        builder.Property(c => c.Content)
            .HasColumnType("varchar(255)")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(c => c.Like)
            .HasColumnType("int");

        builder.Property(c => c.Unlike)
            .HasColumnType("int");

        builder.HasOne<Post>()
            .WithMany(c => c.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<User>()
            .WithMany(c => c.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.SetNull);

    }
}