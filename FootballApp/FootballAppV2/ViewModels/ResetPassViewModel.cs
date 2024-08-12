using System.ComponentModel.DataAnnotations;

namespace FootballAppV2.ViewModels
{
    public class ResetPassViewModel
    {
        [Required (ErrorMessage = "Email obligatorio")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar password")]
        [Compare("Password", ErrorMessage = "Password y su campo de confirmacion deben ser iguales")]
        public string ConfirmPassword { get; set;}

        public string Token { get; set; }
    }
}
