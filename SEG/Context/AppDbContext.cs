using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SEG.Models;
using System.Reflection.Emit;

namespace SEG.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // * USUARIO * \\
                builder.Entity<Usuario>()
                    .HasIndex(u => u.Nome)
                    .IsUnique()
                    .HasDatabaseName("IX_Usuarios_Nome");

                //Login como Unique
                builder.Entity<Usuario>()
                    .HasIndex(u => u.Login)
                    .IsUnique()
                    .HasDatabaseName("IX_Login_Nome");

                //DataCadastro com expressão padrão
                builder.Entity<Usuario>()
                    .Property(u => u.DataCadastro)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                //Status com expressão padrão
                builder.Entity<Usuario>()
                    .Property(u => u.Status)
                    .HasDefaultValue(true);

        }
    }
}
