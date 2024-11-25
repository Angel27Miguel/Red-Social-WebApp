
using RedSocialWebApp.Core.Application.ViewModels.Usuario;

namespace RedSocialWebApp.Core.Application.ViewModels.Amistad
{
	public class AmistadViewModel
	{
		public int Id { get; set; }
		public int UsuarioID { get; set; }
		public int AmigoID { get; set; }
		public UsuarioViewModel Usuario { get; set; }
		public UsuarioViewModel Amigo { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioFotoPerfil { get; set; }
    }
}
