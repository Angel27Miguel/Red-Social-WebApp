using AutoMapper;
using Microsoft.AspNetCore.Http;
using RedSocialWebApp.Core.Application.Helpers;
using RedSocialWebApp.Core.Application.Interfaces.Repositories;
using RedSocialWebApp.Core.Application.Interfaces.Services;
using RedSocialWebApp.Core.Application.ViewModels.Amistad;
using RedSocialWebApp.Core.Application.ViewModels.Publicaciones;
using RedSocialWebApp.Core.Application.ViewModels.Usuario;
using RedSocialWebApp.Core.Domain.Entities;

namespace RedSocialWebApp.Core.Application.Services
{
    public class AmistadService : GenericService<SaveAmistadViewModel, AmistadViewModel, Amistad>, IAmistadService
    {
        private readonly IAmistadRepository _amistadRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsuarioViewModel userViewModel;
        public AmistadService(IAmistadRepository amistadRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
            : base(amistadRepository, mapper)
        {
            _amistadRepository = amistadRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("user");
        }
        public async Task<List<int>> GetAmigosIds()
        {
            if (userViewModel == null) return new List<int>();

            var amistades = await _amistadRepository.GetAmigosByUsuarioIdAsync(userViewModel.Id);
            return amistades;
        }
     
        public async Task<List<AmistadViewModel>> GetAllViewModel()
        {
            if (userViewModel == null)
            {
                return new List<AmistadViewModel>();
            }

            // Obtener todas las amistades que incluyan la información del Usuario
            var amistadList = await _amistadRepository.GetAmigosWithUsuariosByUsuarioIdAsync(userViewModel.Id);

            return amistadList
                .Select(amistad => new AmistadViewModel
                {
                    Id = amistad.Id,
                    UsuarioNombre = amistad.Amigo != null ? amistad.Amigo.Nombre : "Desconocido",
                    UsuarioFotoPerfil = amistad.Amigo != null ? amistad.Amigo.FotoPerfil : "ruta/por/defecto.jpg"
                })
                .ToList();
        }


        public override async Task<SaveAmistadViewModel> Add(SaveAmistadViewModel vm)
        {
            vm.UsuarioID = userViewModel.Id;
            return await base.Add(vm);
        }

    }
}
