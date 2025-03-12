using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using proyecto.Models;

namespace proyecto.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pqrs> Pqrs { get; set; }
        public DbSet<Category> Category { get; set; }  
        public DbSet<Answer> Answer { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Departament> Departament { get; set; }
        public DbSet<Followup> Followup { get; set; }
        public DbSet<CareStaff> CareStaff { get; set; }
        public DbSet<Models.State> State { get; set; }

    }
}
