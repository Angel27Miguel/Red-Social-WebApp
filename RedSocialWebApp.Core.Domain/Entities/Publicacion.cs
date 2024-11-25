using RedSocialWebApp.Core.Domain.Common;

namespace RedSocialWebApp.Core.Domain.Entities
{
    public class Publicacion : AuditableBaseEntity
    {
        public int UsuarioID { get; set; }
        public string Contenido { get; set; }
        public string? Imagen { get; set; }
        public string? VideoYouTube { get; set; }

        // Relaciones
        public Usuario Usuario { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
    }

}
