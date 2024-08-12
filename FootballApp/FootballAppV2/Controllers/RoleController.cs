using FootballAppV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FootballAppV2.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _gestionRoles;
        
        public RoleController(RoleManager<IdentityRole> gestionRoles)
        {
            _gestionRoles = gestionRoles;
        }

        [HttpGet]
        public IActionResult CrearRol()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearRol(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.NombreRol
                };
                IdentityResult result = await _gestionRoles.CreateAsync(identityRole);
                if (result.Succeeded) 
                {
                    return RedirectToAction("index", "home");
                }       
                foreach (IdentityError error in result.Errors) 
                {
                    ModelState.AddModelError("", error.Description);
                }  
            }
            return View(model);
        }
    }
}
