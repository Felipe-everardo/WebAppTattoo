using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppTattoo.Models;

namespace WebAppTattoo.Data
{
    public class WebAppTattooContext : DbContext
    {
        public WebAppTattooContext (DbContextOptions<WebAppTattooContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Client { get; set; } = default!;
        public DbSet<Tattoo> Tattoo { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura a precisão e escala da propriedade ValuePaid
            modelBuilder.Entity<Tattoo>()
                .Property(t => t.ValuePaid)
                .HasPrecision(18, 2); // 18 dígitos no total, 2 após a vírgula

            base.OnModelCreating(modelBuilder);
        }
    }
}
