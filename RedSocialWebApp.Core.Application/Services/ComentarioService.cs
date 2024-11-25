using AutoMapper;
using Microsoft.AspNetCore.Http;
using RedSocialWebApp.Core.Application.Helpers;
using RedSocialWebApp.Core.Application.Interfaces.Repositories;
using RedSocialWebApp.Core.Application.Interfaces.Services;
using RedSocialWebApp.Core.Application.ViewModels.Comentario;
using RedSocialWebApp.Core.Application.ViewModels.Usuario;
using RedSocialWebApp.Core.Domain.Entities;

namespace RedSocialWebApp.Core.Application.Services
{
    public class ComentarioService : GenericService<SaveComentarioViewModel, ComentarioViewModel, Comentario>, IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsuarioViewModel userViewModel;

        public ComentarioService(IComentarioRepository comentarioRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
            : base(comentarioRepository, mapper)
        {
            _comentarioRepository = comentarioRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("user");
        }


        public override async Task<SaveComentarioViewModel> Add(SaveComentarioViewModel vm)
        {
            vm.UsuarioID = userViewModel.Id;
            return await base.Add(vm);
        }
        public async Task<List<ComentarioViewModel>> GetAllViewModel()
        {
            var comentarioList = await _comentarioRepository.GetAllAsync();

            if (userViewModel == null)
            {
                return new List<ComentarioViewModel>();
            }

            return comentarioList
                .Where(comentario => comentario.UsuarioID == userViewModel.Id)
                .OrderByDescending(comentario => comentario.Created)
                .Select(comentario => new ComentarioViewModel
                {
                    Id = comentario.Id,
                    Contenido = comentario.Contenido,
                    Created = comentario.Created,
                    UsuarioNombre = comentario.Usuario != null ? comentario.Usuario.Nombre : "Desconocido",
                    UsuarioFotoPerfil = comentario.Usuario != null ? comentario.Usuario.FotoPerfil : "ruta/por/defecto.jpg"
                })
                .ToList();
        }
    }
}
