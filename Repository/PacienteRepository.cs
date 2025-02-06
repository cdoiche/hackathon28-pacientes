using Hackathon28.Pacientes.Context;
using Hackathon28.Pacientes.Entities;

namespace Hackathon28.Pacientes.Repository
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly PacienteDbContext _context;

        public PacienteRepository(PacienteDbContext context) => _context = context;

        public async Task<Paciente?> GetPacienteById(string id)
        {
            return await _context.Paciente.FindAsync(id);
        }
    }
}
