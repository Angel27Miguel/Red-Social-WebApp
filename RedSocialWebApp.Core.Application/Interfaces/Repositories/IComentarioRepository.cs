using RedSocialWebApp.Core.Domain.Entities;

namespace RedSocialWebApp.Core.Application.Interfaces.Repositories
{
	public interface IComentarioRepository : IGenericRepository<Comentario>
	{
        Task<List<Comentario>> GetByPublicacionIdAsync(int publicacionId);
    }
}
