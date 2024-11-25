using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedSocialWebApp.Core.Application.Interfaces.Repositories;
using RedSocialWebApp.Infrastructure.Persistence.Contexts;
using RedSocialWebApp.Infrastucture.Persistence.Repositories;


namespace RedSocialWebApp.Infrastucture.Persistence
{

    //Extension Method - Decorator
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            #region Contexts
            services.AddDbContext<ApplicationContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAmistadRepository, AmistadRepository>();
            services.AddTransient<IComentarioRepository, ComentarioRepository>();
            services.AddTransient<IComentarioRespuestaRepository, ComentarioRespuestaRepository>();
            services.AddTransient<IPublicacionRepository, PublicacionRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            #endregion
        }
    }
}
