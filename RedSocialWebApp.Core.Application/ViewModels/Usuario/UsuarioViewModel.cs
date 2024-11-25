using RedSocialWebApp.Core.Application.ViewModels.Amistad;
using RedSocialWebApp.Core.Application.ViewModels.Comentario;
using RedSocialWebApp.Core.Application.ViewModels.ComentarioRespuesta;
using RedSocialWebApp.Core.Application.ViewModels.Publicaciones;

namespace RedSocialWebApp.Core.Application.ViewModels.Usuario
{
    public class UsuarioViewModel
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string Telefono { get; set; }
		public string Correo { get; set; }
		public string NombreUsuario { get; set; }
		public string Contraseña { get; set; }
		public string ConfirmarContraseña { get; set; }
		public string? FotoPerfil { get; set; }
		public bool? Activo { get; set; }
        public string? ActivationToken { get; set; }

        // Relaciones
        public ICollection<PublicacionViewModel> Publicaciones { get; set; }
        public ICollection<ComentarioViewModel> Comentarios { get; set; }
        public ICollection<AmistadViewModel> Amistades { get; set; }
        public ICollection<ComentarioRespuestaViewModel> Respuestas { get; set; }
    }
}
