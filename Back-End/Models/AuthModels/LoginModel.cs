using System.ComponentModel.DataAnnotations;

namespace Back_End.Models.AuthModels
{
    public class LoginModel
    {
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}