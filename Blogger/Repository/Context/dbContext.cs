using Blogger.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Repository.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Authors");

                entity.HasKey(a => a.Id);

                entity.Property(a => a.Name).IsRequired().HasMaxLength(150);
                entity.Property(a => a.Email).IsRequired().HasMaxLength(200);
                entity.Property(a => a.Password).IsRequired();

                entity.HasMany(a => a.Posts)
                      .WithOne(p => p.Author)
                      .HasForeignKey(p => p.AuthorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Posts");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Title).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Content).IsRequired();
                entity.Property(p => p.CreatedAt).IsRequired();
            });
        }
    }
}
