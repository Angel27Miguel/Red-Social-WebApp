using RedSocialWebApp.Core.Domain.Entities;

namespace RedSocialWebApp.Core.Application.Interfaces.Repositories
{
	public interface IPublicacionRepository : IGenericRepository<Publicacion>
	{
        Task<Usuario> GetUsuarioByIdAsync(int id);

    }
}
