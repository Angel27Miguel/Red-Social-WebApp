using RedSocialWebApp.Core.Application.ViewModels.ComentarioRespuesta;
using RedSocialWebApp.Core.Domain.Entities;

namespace RedSocialWebApp.Core.Application.Interfaces.Services
{
    public interface IComentarioRespuestaService : IGenericService<SaveComentarioRespuestaViewModel, ComentarioRespuestaViewModel, ComentarioRespuesta>
    {
        
    }
}
