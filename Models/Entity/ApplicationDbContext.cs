
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace B_U2_S2_G5_PROJECT.Models.Entity
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Prenotazione> Prenotazioni { get; set; }
        public DbSet<Camera> Camere { get; set; }

    }
}
