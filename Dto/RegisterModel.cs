using System.ComponentModel.DataAnnotations;

namespace Hackathon28.Pacientes.Dto
{
    public class RegisterModel
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
