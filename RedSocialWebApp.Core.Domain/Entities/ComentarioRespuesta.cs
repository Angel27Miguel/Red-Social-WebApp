using RedSocialWebApp.Core.Domain.Common;

namespace RedSocialWebApp.Core.Domain.Entities
{
    public class ComentarioRespuesta : AuditableBaseEntity
    {

        public int UsuarioID { get; set; }
        public int ComentarioPrincipalId { get; set; }
        public int? ComentarioRespuestaId { get; set; }
        public string Respuesta { get; set; }

        public Usuario Usuario { get; set; }
        public Comentario ComentarioPrincipal { get; set; }
        public Comentario ComentarioRelacionado { get; set; }
    }
}
