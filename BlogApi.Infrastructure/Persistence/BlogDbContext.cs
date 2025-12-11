using Microsoft.EntityFrameworkCore;
using BlogApi.Domain.Entities;

namespace BlogApi.Infrastructure.Persistence;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }

    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Conteudo).IsRequired();
            entity.Property(e => e.DataCriacao).IsRequired();
            entity.Property(e => e.DataAtualizacao);

            entity.HasMany(e => e.Comentarios)
                  .WithOne(e => e.BlogPost)
                  .HasForeignKey(e => e.BlogPostId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Conteudo).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Autor).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DataCriacao).IsRequired();
            entity.Property(e => e.BlogPostId).IsRequired();
        });
    }
}
