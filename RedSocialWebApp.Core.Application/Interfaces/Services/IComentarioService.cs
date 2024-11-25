using RedSocialWebApp.Core.Application.ViewModels.Comentario;
using RedSocialWebApp.Core.Domain.Entities;

namespace RedSocialWebApp.Core.Application.Interfaces.Services
{
    public interface IComentarioService : IGenericService<SaveComentarioViewModel, ComentarioViewModel, Comentario>
    {
    }
}
