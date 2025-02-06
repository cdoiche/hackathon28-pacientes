using Hackathon28.Pacientes.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon28.Pacientes.Controllers
{
    [Route("/pacientes")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private IPacienteRepository _pacienteRepository;
        public PacienteController(IPacienteRepository pacienteRepository) 
        {
            _pacienteRepository = pacienteRepository;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPacienteById(string id)
        {
            var paciente = await _pacienteRepository.GetPacienteById(id);

            if (paciente == null) return NotFound("Paciente não encontrado.");
            
            return Ok(paciente);
        }
    }
}
