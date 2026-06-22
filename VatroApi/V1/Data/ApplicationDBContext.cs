using Microsoft.EntityFrameworkCore;
using VatroApi.V1.Entities;
using VatroApi.V1.Models;

namespace VatroApi.V1.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(
            DbContextOptions<ApplicationDBContext> options
        ) : base(options)
        {

        }

        DbSet<Client> Clients { get; set; }
        DbSet<Control> Controls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                        .HasMany(c => c.Controls)
                        .WithOne(c => c.Client)
                        .HasForeignKey(c => c.ClientId)
                        .OnDelete(DeleteBehavior.Cascade);

            // modelBuilder.Entity<Control>()
            //             .HasOne(c => c.Client)
            //             .WithMany(c => c.Controls)
            //             .HasForeignKey(c => c.ClientId)
            //             .OnDelete(DeleteBehavior.Cascade);
        }

    }
}