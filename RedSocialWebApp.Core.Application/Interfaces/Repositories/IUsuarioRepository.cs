using RedSocialWebApp.Core.Application.ViewModels.Usuario;
using RedSocialWebApp.Core.Domain.Entities;

namespace RedSocialWebApp.Core.Application.Interfaces.Repositories
{
	public interface IUsuarioRepository : IGenericRepository<Usuario>
	{
		Task<Usuario> LoginAsync(LoginViewModel loginVm);
		Task<bool> UserNameExistsAsync(string nombreUsuario);
		Task<Usuario> GetByUserNameAsync(string nombreUsuario);
        Task SaveActivationToken(int id, string token);
		Task<Usuario> GetByTokenAsync(string token);
    }
}
