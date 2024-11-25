using Microsoft.EntityFrameworkCore;
using RedSocialWebApp.Core.Application.Interfaces.Repositories;
using RedSocialWebApp.Core.Domain.Entities;
using RedSocialWebApp.Infrastructure.Persistence.Contexts;

namespace RedSocialWebApp.Infrastucture.Persistence.Repositories
{
    public class ComentarioRepository : GenericRepository<Comentario> , IComentarioRepository
    {
        private readonly ApplicationContext _dbContext;
        public ComentarioRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Comentario>> GetByPublicacionIdAsync(int publicacionId)
        {
            return await _dbContext.Comentarios
                .Where(c => c.PublicacionID == publicacionId)
                .ToListAsync();
        }
        public async Task<List<Comentario>> GetAllAsync()
        {
            return await _dbContext.Comentarios
                .Include(comentario => comentario.Usuario)
                .ToListAsync();
        }
    }
}
