using Microsoft.AspNetCore.Mvc;
using RedSocialWebApp.Core.Application.Interfaces.Services;
using RedSocialWebApp.Core.Application.ViewModels.Amistad;
using RedSocialWebApp.Core.Application.ViewModels.Comentario;
using RedSocialWebApp.Core.Application.ViewModels.Publicaciones;
using RedSocialWebApp.Core.Application.ViewModels.Usuario;
using RedSocialWebApp.Middlewares;

namespace RedSocialWebApp.Controllers
{
    public class AmistadController : Controller
    {
        private readonly ILogger<AmistadController> _logger;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IPublicacionService _publicacionService;
        private readonly IAmistadService _amistadService;
        private readonly IUsuarioService _usuarioService; 
        private readonly IComentarioService _comentarioService;

        public AmistadController(ILogger<AmistadController> logger,
                                 ValidateUserSession validateUserSession,
                                 IPublicacionService publicacionService,
                                 IAmistadService amistadService,
                                 IUsuarioService usuarioService, IComentarioService comentarioService) 
        {
            _logger = logger;
            _validateUserSession = validateUserSession;
            _publicacionService = publicacionService;
            _amistadService = amistadService;
            _usuarioService = usuarioService; 
            _comentarioService = comentarioService;
        }
        public async Task<IActionResult> Index(string searchTerm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            List<UsuarioViewModel> usuarios = new List<UsuarioViewModel>();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                usuarios = await _usuarioService.GetAllViewModel();
                usuarios = usuarios.Where(u => u.Nombre.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                ViewData["SearchTerm"] = searchTerm;
            }

            return View(usuarios);
        }



        public async Task<IActionResult> AmigoPublicaciones()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            // Obtener el ID del usuario logueado
            var usuarioId = _validateUserSession.GetUserId();
            if (usuarioId == null)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            // Obtener IDs de amigos del usuario logueado
            var amigosIds = await _amistadService.GetAmigosIds();
            if (amigosIds == null || !amigosIds.Any())
            {
                return View(new List<PublicacionViewModel>());
            }

            // Obtener publicaciones de amigos
            var publicacionesAmigos = await _publicacionService.GetPublicacionesDeAmigos(amigosIds);

            return View(publicacionesAmigos);
        }
        public IActionResult Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }


            return View("Agregar", new SaveAmistadViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Create(int UsuarioId)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var usuarioId = _validateUserSession.GetUserId();
            if (usuarioId == null)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var amistadVm = new SaveAmistadViewModel
            {
                UsuarioID = usuarioId.Value,
                AmigoID = UsuarioId 
            };

            await _amistadService.Add(amistadVm);

            TempData["SuccessMessage"] = "Amigo agregado exitosamente.";
            return RedirectToRoute(new { controller = "Amistad", action = "Index" });
        }

        public async Task<IActionResult> ListAmigo()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            // Obtener el ID del usuario logueado
            var usuarioId = _validateUserSession.GetUserId();
            if (usuarioId == null)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            // Obtener lista de amigos del usuario logueado
            var amigos = await _amistadService.GetAllViewModel();

            return View("ListAmigos", amigos);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _amistadService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _amistadService.Delete(id);
            return RedirectToAction("ListAmigo");
        }

        public async Task<IActionResult> AddComentario(int PublicacionId, string Contenido)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!string.IsNullOrEmpty(Contenido))
            {
                SaveComentarioViewModel comentario = new()
                {
                    Contenido = Contenido,
                    PublicacionID = PublicacionId
                };

                await _comentarioService.Add(comentario);
            }

            return RedirectToAction("AmigoPublicaciones");
        }
    }

}

