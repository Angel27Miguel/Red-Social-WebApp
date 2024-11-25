using RedSocialWebApp.Core.Domain.Entities;

namespace RedSocialWebApp.Core.Application.Interfaces.Repositories
{
    public interface IAmistadRepository : IGenericRepository<Amistad>
    {
       
        Task<List<int>> GetAmigosByUsuarioIdAsync(int id);
        Task<List<Amistad>> GetAmigosWithUsuariosByUsuarioIdAsync(int id);
    }
}
