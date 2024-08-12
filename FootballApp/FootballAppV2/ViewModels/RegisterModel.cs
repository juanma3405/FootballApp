using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FootballAppV2.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email obligatorio")]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",ErrorMessage ="Formato incorrecto")]
        [EmailAddress]
        [Remote(action:"ComprobarEmail", controller:"UserAccount")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password obligatorio")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repetir Password")]
        [Compare("Password",ErrorMessage = "La Password y la Password de confirmación no coinciden.")]
        public string ConfirmPassword { get; set;}
    }
}
