using RedSocialWebApp.Core.Application.ViewModels.Comentario;
using RedSocialWebApp.Core.Application.ViewModels.Usuario;

namespace RedSocialWebApp.Core.Application.ViewModels.Publicaciones
{
	public class PublicacionViewModel
	{
		public int Id { get; set; }
		public int UsuarioID { get; set; }
		public string Contenido { get; set; }
		public string? Imagen { get; set; }
		public string? VideoYouTube { get; set; }
		public UsuarioViewModel Usuario { get; set; }
        public List<ComentarioViewModel> Comentarios { get; set; } = new List<ComentarioViewModel>();
        public string UsuarioNombre { get; set; }
        public string UsuarioFotoPerfil { get; set; }
        public DateTime? Created { get; set; }

    }
}
