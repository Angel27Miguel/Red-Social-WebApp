using RedSocialWebApp.Core.Domain.Common;

namespace RedSocialWebApp.Core.Domain.Entities
{
    public class Usuario : AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string? FotoPerfil { get; set; }
        public bool? Activo { get; set; }

        public string? ActivationToken { get; set; }

        // Relaciones
        public ICollection<Publicacion> Publicaciones { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
        public ICollection<Amistad> Amistades { get; set; }
        public ICollection<ComentarioRespuesta> Respuestas { get; set; }
    }
}

