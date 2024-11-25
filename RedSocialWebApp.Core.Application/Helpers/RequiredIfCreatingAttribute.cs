using RedSocialWebApp.Core.Application.ViewModels.Usuario;
using System.ComponentModel.DataAnnotations;

namespace RedSocialWebApp.Core.Application.Helpers
{
	public class RequiredIfCreatingAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var userViewModel = (SaveUsuarioViewModel)validationContext.ObjectInstance;

			// Se requiere contraseña solo si se esta creando un nuevo usuario
			if (userViewModel.Id == 0 && string.IsNullOrEmpty(value as string))
			{
				return new ValidationResult(ErrorMessage);
			}

			// Si se esta editando y se proporciona un valor vacio, no se requiere la contraseña
			return ValidationResult.Success;
		}
	}
}