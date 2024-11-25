using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RedSocialWebApp.Core.Application.ViewModels.Publicaciones
{
	public class SavePublicacionViewModel
	{
		public int Id { get; set; }
		public int UsuarioID { get; set; }
		public string Contenido { get; set; }
		public string? Imagen { get; set; }

		[DataType(DataType.Upload)]
		public IFormFile? File { get; set; }
		public string? VideoYouTube { get; set; }
	}
}
