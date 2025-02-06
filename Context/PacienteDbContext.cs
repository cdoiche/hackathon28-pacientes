using Hackathon28.Pacientes.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hackathon28.Pacientes.Context
{
    public class PacienteDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public PacienteDbContext(DbContextOptions<PacienteDbContext> options) : base(options) {}
        
        public DbSet<Paciente> Paciente { get; set; }
    }
}
