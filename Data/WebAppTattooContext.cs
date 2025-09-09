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
    }
}
