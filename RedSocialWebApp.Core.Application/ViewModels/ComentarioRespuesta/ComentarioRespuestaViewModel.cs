using RedSocialWebApp.Core.Application.ViewModels.Comentario;
using RedSocialWebApp.Core.Application.ViewModels.Usuario;

namespace RedSocialWebApp.Core.Application.ViewModels.ComentarioRespuesta
{
    public class ComentarioRespuestaViewModel
    {
        public int Id { get; set; }
        public int UsuarioID { get; set; }
        public int ComentarioPrincipalId { get; set; }
        public int ComentarioRespuestaId { get; set; }
        public string Respuesta { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioFotoPerfil { get; set; }
        public DateTime? Created { get; set; }

        public UsuarioViewModel Usuario { get; set; }
        public ComentarioViewModel ComentarioPrincipal { get; set; }
        public ComentarioViewModel ComentarioRelacionado { get; set; }
    }
}
