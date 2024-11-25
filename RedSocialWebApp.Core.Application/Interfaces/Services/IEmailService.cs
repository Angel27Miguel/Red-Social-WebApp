using RedSocialWebApp.Core.Application.DTOs.Email;
using RedSocialWebApp.Core.Domain.Settings;

namespace RedSocialWebApp.Core.Application.Interfaces.Services
{
	public interface IEmailService
	{
		public MailSettings MailSettings { get; }
		Task SendAsync(EmailRequest request);
	}
}
