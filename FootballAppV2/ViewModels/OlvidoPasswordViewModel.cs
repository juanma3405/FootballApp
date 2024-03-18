using System.ComponentModel.DataAnnotations;

namespace FootballAppV2.ViewModels
{
    public class OlvidoPasswordViewModel
    {
        [Required(ErrorMessage = "Email obligatorio")]
        [EmailAddress(ErrorMessage = "Email con formato incorrecto")]
        public string Email { get; set; }
    }
}
 