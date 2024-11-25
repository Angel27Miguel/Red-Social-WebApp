using AutoMapper;
using RedSocialWebApp.Core.Application.DTOs.Email;
using RedSocialWebApp.Core.Application.Interfaces.Repositories;
using RedSocialWebApp.Core.Application.Interfaces.Services;
using RedSocialWebApp.Core.Application.ViewModels.Usuario;
using RedSocialWebApp.Core.Domain.Entities;

namespace RedSocialWebApp.Core.Application.Services
{
	public class UsuariorService : GenericService<SaveUsuarioViewModel, UsuarioViewModel, Usuario>, IUsuarioService
	{
		private readonly IUsuarioRepository _userRepository;
		private readonly IEmailService _emailService;
		private readonly IMapper _mapper;

		public UsuariorService(IUsuarioRepository userRepository, IEmailService emailService, IMapper mapper) : base(userRepository, mapper)
		{
			_userRepository = userRepository;
			_emailService = emailService;
			_mapper = mapper;
		}

		public async Task<UsuarioViewModel> Login(LoginViewModel vm)
		{

			Usuario user = await _userRepository.LoginAsync(vm);

			if (user == null)
			{
				return null;
			}

			UsuarioViewModel userVm = _mapper.Map<UsuarioViewModel>(user);
			return userVm;
		}

        public override async Task Update(SaveUsuarioViewModel vm, int id)
        {
            var usuario = await _userRepository.GetByIdAsync(id);
            if (usuario == null) return;

            usuario.Nombre = vm.Nombre;
            usuario.Apellido = vm.Apellido;
            usuario.NombreUsuario = vm.NombreUsuario;
            usuario.Correo = vm.Correo;
            usuario.Telefono = vm.Telefono;
            usuario.FotoPerfil = vm.FotoPerfil;
			usuario.Contraseña = vm.Contraseña;

            await _userRepository.UpdateAsync(usuario, id);
        }


        public async Task<SaveUsuarioViewModel> Add(SaveUsuarioViewModel vm)
        {
            // Crear el usuario como inactivo
            vm.Activo = false; 
            SaveUsuarioViewModel userVm = await base.Add(vm);

            // Generar un token unico para la activacion
            string token = Guid.NewGuid().ToString(); 
            await _userRepository.SaveActivationToken(userVm.Id, token); 

            string activationLink = $"https://localhost:7012/User/Activate?token={token}";


            await _emailService.SendAsync(new EmailRequest
            {
                To = userVm.Correo,
                Subject = "Activación de cuenta",
                Body = $"<h1>Activa tu cuenta</h1><p>Haz clic en el siguiente enlace para activar tu cuenta:</p><a href='{activationLink}'>Activar cuenta</a>"
            });

            return userVm;
        }


        public async Task<UsuarioViewModel> GetById(int id)
		{
			var usuario = await _userRepository.GetByIdAsync(id);
			if (usuario == null)
			{
				return null;
			}
			return new UsuarioViewModel
			{
				Id = usuario.Id,
				Nombre = usuario.Nombre,
				Apellido = usuario.Apellido,
				NombreUsuario = usuario.NombreUsuario,
				Contraseña = usuario.Contraseña,
				Correo = usuario.Correo,
				Telefono = usuario.Telefono,
				FotoPerfil = usuario.FotoPerfil,
			};
		}


	}
}
