using AutoMapper;
using Microsoft.AspNetCore.Http;
using RedSocialWebApp.Core.Application.Helpers;
using RedSocialWebApp.Core.Application.Interfaces.Repositories;
using RedSocialWebApp.Core.Application.Interfaces.Services;
using RedSocialWebApp.Core.Application.ViewModels.ComentarioRespuesta;
using RedSocialWebApp.Core.Application.ViewModels.Usuario;
using RedSocialWebApp.Core.Domain.Entities;

namespace RedSocialWebApp.Core.Application.Services
{
	public class ComentarioRespuestaService : GenericService<SaveComentarioRespuestaViewModel, ComentarioRespuestaViewModel, ComentarioRespuesta>, IComentarioRespuestaService
	{
        private readonly IComentarioRespuestaRepository _comentarioRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsuarioViewModel userViewModel;

        public ComentarioRespuestaService(IComentarioRespuestaRepository comentarioRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
            : base(comentarioRepository, mapper)
        {
            _comentarioRepository = comentarioRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("user");
        }


        public override async Task<SaveComentarioRespuestaViewModel> Add(SaveComentarioRespuestaViewModel vm)
        {
            vm.UsuarioID = userViewModel.Id; 
            var comentarioRespuesta = new ComentarioRespuesta
            {
                UsuarioID = vm.UsuarioID,
                Respuesta = vm.Respuesta,
                ComentarioPrincipalId = vm.ComentarioPrincipalId,
                ComentarioRespuestaId = vm.ComentarioRespuestaId 
            };
            await _comentarioRepository.AddAsync(comentarioRespuesta);

            return vm; 
        }

        public async Task<List<ComentarioRespuestaViewModel>> GetAllViewModel()
        {
            var comentarioList = await _comentarioRepository.GetAllAsync();

            if (userViewModel == null)
            {
                return new List<ComentarioRespuestaViewModel>();
            }

            return comentarioList
                .Where(comentario => comentario.UsuarioID == userViewModel.Id)
                .OrderByDescending(comentario => comentario.Created)
                .Select(comentario => new ComentarioRespuestaViewModel
                {
                    Id = comentario.Id,
                    Respuesta = comentario.Respuesta,
                    Created = comentario.Created,
                    UsuarioNombre = comentario.Usuario != null ? comentario.Usuario.Nombre : "Desconocido",
                    UsuarioFotoPerfil = comentario.Usuario != null ? comentario.Usuario.FotoPerfil : "ruta/por/defecto.jpg"
                })
                .ToList();
        }
    }
    
}
