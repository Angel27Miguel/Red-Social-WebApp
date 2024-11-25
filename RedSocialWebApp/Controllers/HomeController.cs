using Microsoft.AspNetCore.Mvc;
using RedSocialWebApp.Core.Application.Interfaces.Services;
using RedSocialWebApp.Core.Application.ViewModels.Comentario;
using RedSocialWebApp.Core.Application.ViewModels.Publicaciones;
using RedSocialWebApp.Middlewares;

namespace RedSocialWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IComentarioService _comentarioService;
        private readonly IPublicacionService _publicacionService;

        public HomeController(ILogger<HomeController> logger, ValidateUserSession validateUserSession, IPublicacionService publicacionService, IComentarioService comentarioService)
        {
            _logger = logger;
            _validateUserSession = validateUserSession;
            _publicacionService = publicacionService;
            _comentarioService = comentarioService;
        }


        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var publicaciones = await _publicacionService.GetAllViewModel();
            return View(publicaciones);
        }

        public IActionResult Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View("CrearPublicacion", new SavePublicacionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePublicacionViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (ModelState.IsValid)
            {
                
                if (vm.File != null && vm.File.Length > 0)
                {
                    vm.Imagen = await UploadFile(vm.File);
                }

                await _publicacionService.Add(vm);
                TempData["SuccessMessage"] = "Publicación creada exitosamente.";
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View("CrearPublicacion", vm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var vm = await _publicacionService.GetByIdSaveViewModel(id);
            if (vm == null)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View("CrearPublicacion", vm);
        }

        private async Task<string> UploadFile(IFormFile file)
        {
            string basePath = $"/Images/Publicaciones/";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            // Crear carpeta si no existe
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Generar nombre de archivo único
            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(path, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"{basePath}{fileName}";
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePublicacionViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("CrearPublicacion", vm);
            }

            var existingPublicacion = await _publicacionService.GetByIdSaveViewModel(vm.Id);

            if (existingPublicacion == null)
            {
                ModelState.AddModelError("", "La publicación no se encontró.");
                return View("CrearPublicacion", vm);
            }

            // Actualiza la imagen si es necesario
            vm.Imagen = UploadFile(vm.File, vm.Id, true, existingPublicacion.Imagen ?? string.Empty);

           
            await _publicacionService.Update(vm, vm.Id);

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }


        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _publicacionService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _publicacionService.Delete(id);

            string basePath = $"/Images/Publicaciones/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

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
            string basePath = $"/Images/Publicaciones/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            
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

			return RedirectToAction("Index");
		}

	}
}
