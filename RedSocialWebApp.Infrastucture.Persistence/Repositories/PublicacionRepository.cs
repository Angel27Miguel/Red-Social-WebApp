using Microsoft.EntityFrameworkCore;
using RedSocialWebApp.Core.Application.Interfaces.Repositories;
using RedSocialWebApp.Core.Domain.Entities;
using RedSocialWebApp.Infrastructure.Persistence.Contexts;

namespace RedSocialWebApp.Infrastucture.Persistence.Repositories
{
    public class PublicacionRepository : GenericRepository<Publicacion>, IPublicacionRepository
    {
        private readonly ApplicationContext _dbContext;
        public PublicacionRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Publicacion>> GetAllAsync()
        {
            return await _dbContext.Publicaciones
                .Include(publicacion => publicacion.Usuario)
                .ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _dbContext.Usuarios.FindAsync(id);
        }
    }
}
