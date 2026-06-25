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

        public DbSet<Client> Clients { get; set; }
        public DbSet<Control> Controls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                        .HasMany(c => c.Controls)
                        .WithOne(c => c.Client)
                        .HasForeignKey(c => c.ClientId)
                        .OnDelete(DeleteBehavior.Cascade);


            // _clientRepository.ClientExists 
            // fixing race condition, The real protection should be a unique database constraint.
            modelBuilder.Entity<Client>()
                        .HasIndex(c => c.Name)
                        .IsUnique();
            // modelBuilder.Entity<Control>()
            //             .HasOne(c => c.Client)
            //             .WithMany(c => c.Controls)
            //             .HasForeignKey(c => c.ClientId)
            //             .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

    }
}