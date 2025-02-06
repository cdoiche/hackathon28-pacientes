using System.ComponentModel.DataAnnotations;

namespace Hackathon28.Pacientes.Dto
{
    public class LoginModel
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
