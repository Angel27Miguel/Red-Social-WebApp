using AutoMapper;
using RedSocialWebApp.Core.Application.ViewModels.Amistad;
using RedSocialWebApp.Core.Application.ViewModels.Comentario;
using RedSocialWebApp.Core.Application.ViewModels.ComentarioRespuesta;
using RedSocialWebApp.Core.Application.ViewModels.Publicaciones;
using RedSocialWebApp.Core.Application.ViewModels.Usuario;
using RedSocialWebApp.Core.Domain.Entities;

namespace RedSocialWebApp.Core.Application.Mappings
{
	public class GeneralProfile : Profile
	{
		public GeneralProfile()
		{
            #region ComentarioRespuestaProfile
            CreateMap<ComentarioRespuesta, ComentarioRespuestaViewModel>()
            .ForMember(x => x.UsuarioNombre, opt => opt.Ignore())
            .ForMember(x => x.UsuarioFotoPerfil, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.Created, opt => opt.Ignore())
            .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<ComentarioRespuesta, SaveComentarioRespuestaViewModel>()
             .ReverseMap()
             .ForMember(x => x.Created, opt => opt.Ignore())
             .ForMember(x => x.CreatedBy, opt => opt.Ignore())
             .ForMember(x => x.LastModified, opt => opt.Ignore())
             .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region ComentarioProfile
            CreateMap<Comentario, ComentarioViewModel>()
			.ForMember(x => x.UsuarioNombre, opt => opt.Ignore())
			.ForMember(x => x.UsuarioFotoPerfil, opt => opt.Ignore())
			.ReverseMap()
			.ForMember(x => x.Created, opt => opt.Ignore())
			.ForMember(x => x.CreatedBy, opt => opt.Ignore())
			.ForMember(x => x.LastModified, opt => opt.Ignore())
			.ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

			CreateMap<Comentario, SaveComentarioViewModel>()
			 .ReverseMap()
			 .ForMember(x => x.Created, opt => opt.Ignore())
			 .ForMember(x => x.CreatedBy, opt => opt.Ignore())
			 .ForMember(x => x.LastModified, opt => opt.Ignore())
			 .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
			#endregion

			#region PublicacionProfile
			CreateMap<Publicacion, PublicacionViewModel>()
			.ForMember(x => x.UsuarioNombre, opt => opt.Ignore())
            .ForMember(x => x.UsuarioFotoPerfil, opt => opt.Ignore())
            .ReverseMap()
			.ForMember(x => x.Created, opt => opt.Ignore())
			.ForMember(x => x.CreatedBy, opt => opt.Ignore())
			.ForMember(x => x.LastModified, opt => opt.Ignore())
			.ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

			CreateMap<Publicacion, SavePublicacionViewModel>()
			.ForMember(x => x.File, opt => opt.Ignore())
			.ReverseMap()
			.ForMember(x => x.Created, opt => opt.Ignore())
			.ForMember(x => x.CreatedBy, opt => opt.Ignore())
			.ForMember(x => x.LastModified, opt => opt.Ignore())
			.ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region AmistadProfile
            CreateMap<Amistad, AmistadViewModel>()
            .ForMember(x => x.UsuarioNombre, opt => opt.Ignore())
            .ForMember(x => x.UsuarioFotoPerfil, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.Created, opt => opt.Ignore())
            .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Amistad, SaveAmistadViewModel>()
             .ReverseMap()
             .ForMember(x => x.Created, opt => opt.Ignore())
             .ForMember(x => x.CreatedBy, opt => opt.Ignore())
             .ForMember(x => x.LastModified, opt => opt.Ignore())
             .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region UserProfile
            CreateMap<Usuario, UsuarioViewModel>()
			.ReverseMap()
			.ForMember(x => x.Created, opt => opt.Ignore())
			.ForMember(x => x.CreatedBy, opt => opt.Ignore())
			.ForMember(x => x.LastModified, opt => opt.Ignore())
			.ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

			CreateMap<Usuario, SaveUsuarioViewModel>()
			.ForMember(x => x.ConfirmarContraseña, opt => opt.Ignore())
			.ForMember(x => x.File, opt => opt.Ignore())
			.ReverseMap()
			.ForMember(x => x.Created, opt => opt.Ignore())
			.ForMember(x => x.CreatedBy, opt => opt.Ignore())
			.ForMember(x => x.LastModified, opt => opt.Ignore())
			.ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
			#endregion
		}
	}
}
