using Hackathon28.Pacientes.Entities;

namespace Hackathon28.Pacientes.Repository
{
    public interface IPacienteRepository
    {
        Task<Paciente?> GetPacienteById(string id);
    }
}
