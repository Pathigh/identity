using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoIdentity2.Models;

namespace ProyectoIdentity2.Datos
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        // agregamos los diferentes modelos que necesitamos

        public DbSet<AppUsuario> AppUsuario { get; set; }
    }
}
