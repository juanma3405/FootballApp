using System.ComponentModel.DataAnnotations;

namespace FootballAppV2.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email obligatorio")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password obligatoria")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Recuerdame")]
        public bool RememberMe { get; set; }
        
        //public string? UrlRetorno { get; set; } 
    }
}
