using Microsoft.AspNetCore.Http;
using RedSocialWebApp.Core.Application.Helpers;
using System.ComponentModel.DataAnnotations;


namespace RedSocialWebApp.Core.Application.ViewModels.Usuario
{
	public class SaveUsuarioViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "El nombre es obligatorio.")]
		public string Nombre { get; set; }

		[Required(ErrorMessage = "El apellido es obligatorio.")]
		public string Apellido { get; set; }

		[Required(ErrorMessage = "El Telefono es obligatorio.")]
		[RegularExpression(@"^(809|829|849)-\d{3}-\d{4}$", ErrorMessage = "El teléfono debe tener el formato 809-123-4567, 829-123-4567, o 849-123-4567.")]
		public string Telefono { get; set; }

		[Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
		public string NombreUsuario { get; set; }
		public string? NombreUsuarioError { get; set; }

		[Required(ErrorMessage = "El correo es obligatorio.")]
		[EmailAddress(ErrorMessage = "Debe ser un correo válido.")]
		public string Correo { get; set; }

		[DataType(DataType.Password)]
		[RequiredIfCreating(ErrorMessage = "La contraseña es obligatoria para el registro.")]
		public string? Contraseña { get; set; }

		[DataType(DataType.Password)]
		[Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden.")]
		public string? ConfirmarContraseña { get; set; }

		public string? FotoPerfil { get; set; }
		public bool? Activo { get; set; }
        public string? ActivationToken { get; set; }

        [DataType(DataType.Upload)]
		public IFormFile? File { get; set; }
	}
}
