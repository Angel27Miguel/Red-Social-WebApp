
namespace RedSocialWebApp.Core.Application.ViewModels.ComentarioRespuesta
{
    public class SaveComentarioRespuestaViewModel
    {
        public int Id { get; set; }
        public int UsuarioID { get; set; }
        public int ComentarioPrincipalId { get; set; }
        public int? ComentarioRespuestaId { get; set; }
        public string Respuesta { get; set; }
    }
}
