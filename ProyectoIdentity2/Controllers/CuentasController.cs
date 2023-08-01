using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoIdentity2.Models;

namespace ProyectoIdentity2.Controllers
{
    public class CuentasController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		public CuentasController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Registro(string returnurl = null)
        {
			ViewData["ReturnUrl"] = returnurl;
			RegistroViewModel registroVM = new RegistroViewModel();
            return View(registroVM);
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Registro(RegistroViewModel rgViewModel, string returnurl = null)
		{
			ViewData["ReturnUrl"] = returnurl;
			returnurl = returnurl ?? Url.Content("~/");
			if (ModelState.IsValid)
            {
                var usuario = new AppUsuario { UserName = rgViewModel.Email, Email = rgViewModel.Email, Nombre = rgViewModel.Nombre, Telefono= rgViewModel.Telefono, Direccion= rgViewModel.Direccion, FechaNacimiento= rgViewModel.FechaNacimiento, Estado= rgViewModel.Estado};
                var resultado = await _userManager.CreateAsync(usuario,rgViewModel.Password);

                if (resultado.Succeeded)
                {
                    await _signInManager.SignInAsync(usuario, isPersistent: false);

					//return RedirectToAction("Index", "Home");
					return LocalRedirect(returnurl);

				}
               
                ValidarErrores(resultado);
            }
            return View(rgViewModel);
		}
        //manejador de errores
        private void ValidarErrores(IdentityResult resultado)
        {
            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(String.Empty, error.Description);
            }
        }

        //Metodo mostrar formulario de acceso

        [HttpGet]
        public IActionResult Acceso(string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Acceso(AccesoViewModel accViewModel, string returnurl = null)
		{
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
			{
				
				var resultado = await _signInManager.PasswordSignInAsync(accViewModel.Email, accViewModel.Password, accViewModel.RememberMe, lockoutOnFailure:true);

				if (resultado.Succeeded)
				{

                    //return RedirectToAction("Index", "Home");
                    //return Redirect(returnurl);
					return LocalRedirect(returnurl); // Proteccion de redireccionamiento abierto
				}
                if (resultado.IsLockedOut)
                {
					return View("Bloqueado");
				}

                else
                {
                    ModelState.AddModelError(String.Empty, "Acceso invalido");
                    return View(accViewModel);
                }

			}
			return View(accViewModel);
		}
        // salir o cerrar sesion de la aplicacion(logout)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalirAplicacion()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index),"Home");
        }


        //Metodo para olvido de contraseña
        [HttpGet]
        public IActionResult OlvidoPassword()
        {
            return View();
        }
	}
}
