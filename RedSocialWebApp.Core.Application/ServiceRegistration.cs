using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedSocialWebApp.Core.Application.Interfaces.Repositories;
using RedSocialWebApp.Core.Application.Interfaces.Services;
using RedSocialWebApp.Core.Application.Services;
using System.Reflection;

namespace RedSocialWebApp.Core.Application
{

	//Extension Method - Decorator
	public static class ServiceRegistration
	{
		public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #region Services
            services.AddScoped<IComentarioRespuestaService, ComentarioRespuestaService>();
            services.AddScoped<IComentarioService, ComentarioService>();
			services.AddScoped<IPublicacionService, PublicacionService>();
            services.AddScoped<IAmistadService, AmistadService>();
            services.AddTransient<IUsuarioService, UsuariorService>();
			#endregion
		}
	}
}
