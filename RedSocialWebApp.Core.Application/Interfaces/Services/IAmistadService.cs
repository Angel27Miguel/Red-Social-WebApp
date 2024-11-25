using RedSocialWebApp.Core.Application.ViewModels.Amistad;
using RedSocialWebApp.Core.Domain.Entities;

namespace RedSocialWebApp.Core.Application.Interfaces.Services
{
    public interface IAmistadService : IGenericService<SaveAmistadViewModel, AmistadViewModel, Amistad>
    {
    
        Task<List<int>> GetAmigosIds();
    }
}
