using Microsoft.AspNetCore.Mvc;
using RedSocialWebApp.Core.Application.DTOs.Email;
using RedSocialWebApp.Core.Application.Helpers;
using RedSocialWebApp.Core.Application.Interfaces.Repositories;
using RedSocialWebApp.Core.Application.Interfaces.Services;
using RedSocialWebApp.Core.Application.ViewModels.Usuario;
using RedSocialWebApp.Middlewares;

namespace RedSocialApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsuarioService _userService;
        private readonly IUsuarioRepository _userRepository;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IEmailService _emailService;

        public UserController(IUsuarioService userService, ValidateUserSession validateUserSession, IUsuarioRepository userRepository, IEmailService emailService)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            UsuarioViewModel userVm = await _userService.Login(vm);

            if (userVm != null)
            {
                // Validar si el usuario esta activo
                if ((bool)!userVm.Activo)
                {
                    ModelState.AddModelError("", "La cuenta no está activada. Por favor, revisa tu correo para activar tu cuenta.");
                    return View(vm);
                }

                HttpContext.Session.Set<UsuarioViewModel>("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation", "Datos de acceso incorrecto");
            }

            return View(vm);
        }


        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult Register()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View("SaveUsuario", new SaveUsuarioViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUsuarioViewModel vm)
        {
            if (await _userRepository.UserNameExistsAsync(vm.NombreUsuario))
            {
                vm.NombreUsuarioError = "El nombre de usuario ya existe. Por favor, elige otro.";
                return View("SaveUsuario", vm);
            }

            if (!ModelState.IsValid)
            {
                return View("SaveUsuario", vm);
            }

            SaveUsuarioViewModel userVm = await _userService.Add(vm);

            if (userVm.Id != 0 && userVm != null)
            {
                userVm.FotoPerfil = UploadFile(vm.File, userVm.Id); 
                await _userService.Update(userVm, userVm.Id); 
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var userVm = HttpContext.Session.Get<UsuarioViewModel>("user");
            if (userVm == null)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }


            SaveUsuarioViewModel vm = await _userService.GetByIdSaveViewModel(userVm.Id);
            return View("SaveUsuario", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUsuarioViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveUsuario", vm);
            }

            SaveUsuarioViewModel userVm = await _userService.GetByIdSaveViewModel(vm.Id);


            if (userVm == null)
            {
                ModelState.AddModelError("", "El usuario no se encontró.");
                return View("SaveUsuario", vm);
            }

            vm.FotoPerfil = UploadFile(vm.File, vm.Id, true, userVm.FotoPerfil ?? string.Empty);

            // Verifica si se ingreso una nueva contraseña
            if (!string.IsNullOrEmpty(vm.Contraseña))
            {
                vm.Contraseña = PasswordEncryptation.ComputeSha256Hash(vm.Contraseña); 
            }
            else
            {
                // Si no se ingreso nueva contraseña, mantenla igual
                vm.Contraseña = userVm.Contraseña;
            }

            await _userService.Update(vm, vm.Id);


            UsuarioViewModel updatedUserVm = await _userService.GetById(vm.Id);
            HttpContext.Session.Set("user", updatedUserVm);

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }



        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Usuarios/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string nombreUsuario)
        {
            var usuario = await _userRepository.GetByUserNameAsync(nombreUsuario);

            if (usuario == null)
            {

                TempData["ErrorMessage"] = "El nombre de usuario no existe.";
                return RedirectToAction("Index");
            }

            // Generar una nueva contraseña 
            string nuevaContraseña = GenerateNewPassword();
            usuario.Contraseña = PasswordEncryptation.ComputeSha256Hash(nuevaContraseña);


            await _userRepository.UpdateAsync(usuario, usuario.Id);

            // Enviar un correo al usuario con la nueva contraseña
            await _emailService.SendAsync(new EmailRequest
            {
                To = usuario.Correo,
                Subject = "Restablecimiento de contraseña",
                Body = $"Saludo {usuario.Nombre}. Su nueva contraseña es: {nuevaContraseña}"
            });

            TempData["SuccessMessage"] = "La contraseña ha sido restablecida y enviada al correo registrado.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Activate(string token)
        {
            var usuario = await _userRepository.GetByTokenAsync(token);
            if (usuario == null)
            {
                // Manejar el caso donde el token no es válido o ya fue utilizado
                return View("Error"); // Puedes redirigir a una vista de error personalizada
            }

            // Activar usuario
            usuario.Activo = true; // Cambia el estado a activo
            usuario.ActivationToken = null; // Limpia el token después de la activación
            await _userRepository.UpdateAsync(usuario, usuario.Id); // Actualizar en la base de datos

            return RedirectToRoute(new { controller = "User", action = "Index" }); // Redirigir al inicio de sesión
        }

        private string GenerateNewPassword()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }


    }
}
