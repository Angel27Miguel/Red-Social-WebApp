using AutoMapper;
using Microsoft.AspNetCore.Http;
using RedSocialWebApp.Core.Application.Helpers;
using RedSocialWebApp.Core.Application.Interfaces.Repositories;
using RedSocialWebApp.Core.Application.Interfaces.Services;
using RedSocialWebApp.Core.Application.ViewModels.Comentario;
using RedSocialWebApp.Core.Application.ViewModels.Publicaciones;
using RedSocialWebApp.Core.Application.ViewModels.Usuario;
using RedSocialWebApp.Core.Domain.Entities;

namespace RedSocialWebApp.Core.Application.Services
{
	public class PublicacionService : GenericService<SavePublicacionViewModel, PublicacionViewModel, Publicacion>, IPublicacionService
	{
		private readonly IPublicacionRepository _publicacionRepository;

		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UsuarioViewModel userViewModel;
        private readonly IComentarioRepository _comentarioRepository;

        public PublicacionService(IPublicacionRepository publicacionRepository, IComentarioRepository comentarioRepository, IHttpContextAccessor httpContextAccessor,
            IMapper mapper) : base(publicacionRepository, mapper)
        {
            _publicacionRepository = publicacionRepository;
            _comentarioRepository = comentarioRepository; 
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("user");
        }


        public override async Task<SavePublicacionViewModel> Add(SavePublicacionViewModel vm)
		{
			vm.UsuarioID = userViewModel.Id;
			return await base.Add(vm);
		}

        public async Task<List<PublicacionViewModel>> GetAllViewModel()
        {
            var publicacionList = await _publicacionRepository.GetAllAsync();

            if (userViewModel == null)
            {
                return new List<PublicacionViewModel>();
            }

            var publicacionesViewModel = new List<PublicacionViewModel>();

            var publicacionesUsuario = publicacionList
                .Where(publicacion => publicacion.UsuarioID == userViewModel.Id)
                .OrderByDescending(publicacion => publicacion.Created)
                .ToList();

            foreach (var publicacion in publicacionesUsuario)
            {
                // Obtener comentarios de la publicacion
                var comentarios = await _comentarioRepository.GetByPublicacionIdAsync(publicacion.Id);

                var publicacionViewModel = new PublicacionViewModel
                {
                    Id = publicacion.Id,
                    Contenido = publicacion.Contenido,
                    Imagen = publicacion.Imagen,
                    VideoYouTube = publicacion.VideoYouTube,
                    UsuarioID = publicacion.UsuarioID,
                    Created = publicacion.Created,
                    UsuarioNombre = publicacion.Usuario != null ? publicacion.Usuario.Nombre : "Desconocido",
                    UsuarioFotoPerfil = publicacion.Usuario != null ? publicacion.Usuario.FotoPerfil : "ruta/por/defecto.jpg",
                    Comentarios = comentarios.Select(comentario => new ComentarioViewModel
                    {
                        Id = comentario.Id,
                        Contenido = comentario.Contenido,
                        UsuarioNombre = comentario.Usuario != null ? comentario.Usuario.Nombre : "Desconocido",
                        UsuarioFotoPerfil = comentario.Usuario != null ? comentario.Usuario.FotoPerfil : "ruta/por/defecto.jpg",
                        Created = comentario.Created
                    }).ToList()
                };

                publicacionesViewModel.Add(publicacionViewModel);
            }

            return publicacionesViewModel;
        }


        public async Task<List<PublicacionViewModel>> GetPublicacionesDeAmigos(List<int> amigosIds)
        {
            if (amigosIds == null || !amigosIds.Any())
            {
                return new List<PublicacionViewModel>();
            }

            var publicacionList = await _publicacionRepository.GetAllAsync();

            var publicacionesViewModel = new List<PublicacionViewModel>();

            var publicacionesAmigos = publicacionList
                .Where(publicacion => amigosIds.Contains(publicacion.UsuarioID))
                .OrderByDescending(publicacion => publicacion.Created)
                .ToList();

            foreach (var publicacion in publicacionesAmigos)
            {
                // Obtener comentarios de la publicacion
                var comentarios = await _comentarioRepository.GetByPublicacionIdAsync(publicacion.Id);

                var publicacionViewModel = new PublicacionViewModel
                {
                    Id = publicacion.Id,
                    Contenido = publicacion.Contenido,
                    Imagen = publicacion.Imagen,
                    VideoYouTube = publicacion.VideoYouTube,
                    UsuarioID = publicacion.UsuarioID,
                    Created = publicacion.Created,
                    UsuarioNombre = publicacion.Usuario != null ? publicacion.Usuario.Nombre : "Desconocido",
                    UsuarioFotoPerfil = publicacion.Usuario != null ? publicacion.Usuario.FotoPerfil : "ruta/por/defecto.jpg",
                    Comentarios = comentarios.Select(comentario => new ComentarioViewModel
                    {
                        Id = comentario.Id,
                        Contenido = comentario.Contenido,
                        UsuarioNombre = comentario.Usuario != null ? comentario.Usuario.Nombre : "Desconocido",
                        UsuarioFotoPerfil = comentario.Usuario != null ? comentario.Usuario.FotoPerfil : "ruta/por/defecto.jpg",
                        Created = comentario.Created
                    }).ToList()
                };

                publicacionesViewModel.Add(publicacionViewModel);
            }

            return publicacionesViewModel;
        }



        public override async Task Update(SavePublicacionViewModel vm, int id)
        {
            var publicacion = await _publicacionRepository.GetByIdAsync(id);
            if (publicacion == null) return;

            publicacion.Contenido = vm.Contenido;
            publicacion.Imagen = vm.Imagen;
            publicacion.VideoYouTube = vm.VideoYouTube;

            await _publicacionRepository.UpdateAsync(publicacion, id);
        }





    }
}
