using RedSocialWebApp.Core.Application.ViewModels.Usuario;
using RedSocialWebApp.Core.Domain.Entities;

namespace RedSocialWebApp.Core.Application.Interfaces.Services
{
	public interface IUsuarioService : IGenericService<SaveUsuarioViewModel, UsuarioViewModel, Usuario>
	{
		Task<UsuarioViewModel> GetById(int id);
		Task<UsuarioViewModel> Login(LoginViewModel vm);
	}
}
