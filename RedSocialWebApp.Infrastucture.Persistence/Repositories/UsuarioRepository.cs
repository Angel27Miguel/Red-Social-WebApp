using Microsoft.EntityFrameworkCore;
using RedSocialWebApp.Core.Application.Helpers;
using RedSocialWebApp.Core.Application.Interfaces.Repositories;
using RedSocialWebApp.Core.Application.ViewModels.Usuario;
using RedSocialWebApp.Core.Domain.Entities;
using RedSocialWebApp.Infrastructure.Persistence.Contexts;

namespace RedSocialWebApp.Infrastucture.Persistence.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationContext _dbContext;

        public UsuarioRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<Usuario> AddAsync(Usuario entity)
        {
            entity.Contraseña = PasswordEncryptation.ComputeSha256Hash(entity.Contraseña);
            await base.AddAsync(entity);
            return entity;
        }
        public async Task UpdateAsync(Usuario entity, int id)
        {
            var existingEntity = await _dbContext.Set<Usuario>().FindAsync(id);
            if (existingEntity != null)
            {
                existingEntity.Contraseña = entity.Contraseña;
                _dbContext.Entry(existingEntity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<Usuario> LoginAsync(LoginViewModel loginVm)
        {
            string passwordEncrypt = PasswordEncryptation.ComputeSha256Hash(loginVm.Contraseña);
            Usuario user = await _dbContext.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.NombreUsuario == loginVm.NombreUsuario && u.Contraseña == passwordEncrypt);
            return user;
        }

        public async Task<bool> UserNameExistsAsync(string nombreUsuario)
        {
            return await _dbContext.Set<Usuario>()
                .AnyAsync(u => u.NombreUsuario == nombreUsuario);
        }

        public async Task<Usuario> GetByUserNameAsync(string nombreUsuario)
        {
            return await _dbContext.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
        }

        public async Task SaveActivationToken(int userId, string token)
        {
            var usuario = await GetByIdAsync(userId);
            if (usuario != null)
            {
                usuario.ActivationToken = token; 
                await UpdateAsync(usuario, userId);
            }
        }

        public async Task<Usuario> GetByTokenAsync(string token)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.ActivationToken == token);
        }

    }
}

