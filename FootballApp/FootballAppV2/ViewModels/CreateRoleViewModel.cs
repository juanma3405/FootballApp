using System.ComponentModel.DataAnnotations;

namespace FootballAppV2.ViewModels
{
    public class CreateRoleViewModel
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Rol")]

        public string NombreRol { get; set; }
    }
}
