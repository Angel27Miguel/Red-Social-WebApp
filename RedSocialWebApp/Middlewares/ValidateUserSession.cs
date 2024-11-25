using RedSocialWebApp.Core.Application.ViewModels.Usuario;
using RedSocialWebApp.Core.Application.Helpers;

namespace RedSocialWebApp.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UsuarioViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("user");
            return userViewModel != null;
        }

        public int? GetUserId()
        {
            UsuarioViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("user");
            return userViewModel?.Id;
        }
    }
}
