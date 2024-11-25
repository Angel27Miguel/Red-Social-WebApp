using RedSocialWebApp.Core.Domain.Common;

namespace RedSocialWebApp.Core.Domain.Entities
{
    public class Comentario : AuditableBaseEntity
    {
        public int PublicacionID { get; set; }
        public int UsuarioID { get; set; }
        public string Contenido { get; set; }

        // Relaciones
        public Publicacion Publicacion { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<ComentarioRespuesta> Respuestas { get; set; } 
        public ICollection<ComentarioRespuesta> ComentariosPrincipales { get; set; } 
    }
}
