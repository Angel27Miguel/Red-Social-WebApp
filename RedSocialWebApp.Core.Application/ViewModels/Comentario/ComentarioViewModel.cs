using RedSocialWebApp.Core.Application.ViewModels.ComentarioRespuesta;
using RedSocialWebApp.Core.Application.ViewModels.Usuario;

namespace RedSocialWebApp.Core.Application.ViewModels.Comentario
{
    public class ComentarioViewModel
	{
		public int Id { get; set; }
		public int PublicacionID { get; set; }
		public int UsuarioID { get; set; }
		public string Contenido { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioFotoPerfil { get; set; }
        public DateTime? Created { get; set; }
        public UsuarioViewModel Usuario { get; set; }
		public List<ComentarioRespuestaViewModel> Respuestas { get; set; }
	}
}
