using Microsoft.EntityFrameworkCore;
using RedSocialWebApp.Core.Application.Interfaces.Repositories;
using RedSocialWebApp.Core.Domain.Entities;
using RedSocialWebApp.Infrastructure.Persistence.Contexts;

namespace RedSocialWebApp.Infrastucture.Persistence.Repositories
{
    public class ComentarioRespuestaRepository : GenericRepository<ComentarioRespuesta>, IComentarioRespuestaRepository
    {
        private readonly ApplicationContext _dbContext;
        public ComentarioRespuestaRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ComentarioRespuesta> AddAsync(ComentarioRespuesta comentarioRespuesta)
        {
            await _dbContext.ComentarioRespuestas.AddAsync(comentarioRespuesta);
            await _dbContext.SaveChangesAsync();
            return comentarioRespuesta;
        }

        public async Task<List<Comentario>> GetAllAsync()
        {
            return await _dbContext.Comentarios
                .Include(comentario => comentario.Usuario)
                .ToListAsync();
        }
    }
}
