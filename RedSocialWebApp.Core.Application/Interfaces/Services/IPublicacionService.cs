using RedSocialWebApp.Core.Application.ViewModels.Publicaciones;
using RedSocialWebApp.Core.Domain.Entities;

namespace RedSocialWebApp.Core.Application.Interfaces.Services
{
	public interface IPublicacionService : IGenericService<SavePublicacionViewModel, PublicacionViewModel, Publicacion>
	{
		
		Task<List<PublicacionViewModel>> GetPublicacionesDeAmigos(List<int> amigosIds);

    }
}
