using System.ComponentModel.DataAnnotations;

namespace Back_End.Models.AuthModels
{
    public class LoginModel
    {
        public string Login { get; set; }

        public string? Password { get; set; }
    }
}