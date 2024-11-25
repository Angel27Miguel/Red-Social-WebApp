using Microsoft.EntityFrameworkCore;
using RedSocialWebApp.Core.Application.Interfaces.Repositories;
using RedSocialWebApp.Core.Domain.Entities;
using RedSocialWebApp.Infrastructure.Persistence.Contexts;

namespace RedSocialWebApp.Infrastucture.Persistence.Repositories
{
    public class AmistadRepository : GenericRepository<Amistad>, IAmistadRepository
    {
        private readonly ApplicationContext _dbContext;
        public AmistadRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Publicacion>> GetAllAsync()
        {
            return await _dbContext.Publicaciones
                .Include(publicacion => publicacion.Usuario)
                .ToListAsync();
        }

        public async Task<List<int>> GetAmigosByUsuarioIdAsync(int usuarioId)
        {
            var amigosIds = await _dbContext.Amistades
                .Where(a => a.UsuarioID == usuarioId || a.AmigoID == usuarioId)
                .Select(a => a.UsuarioID == usuarioId ? a.AmigoID : a.UsuarioID)
                .ToListAsync();

            return amigosIds;
        }

        public async Task<List<Amistad>> GetAmigosWithUsuariosByUsuarioIdAsync(int usuarioId)
        {
            return await _dbContext.Amistades
                .Include(a => a.Usuario)
                .Include(a => a.Amigo)
                .Where(a => a.UsuarioID == usuarioId || a.AmigoID == usuarioId)
                .ToListAsync();
        }

    }
}
