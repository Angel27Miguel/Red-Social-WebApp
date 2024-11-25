using System.ComponentModel.DataAnnotations;

namespace RedSocialWebApp.Core.Application.ViewModels.Usuario
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Debe colocar el nombre de usuario")]
		[DataType(DataType.Text)]
		public string NombreUsuario { get; set; }

		[Required(ErrorMessage = "Debe colocar una contraseña")]
		[DataType(DataType.Password)]
		public string Contraseña { get; set; }
	}
}
