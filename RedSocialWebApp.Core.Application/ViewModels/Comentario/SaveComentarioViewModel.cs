
namespace RedSocialWebApp.Core.Application.ViewModels.Comentario
{
    public class SaveComentarioViewModel
    {
        public int Id { get; set; }
        public int PublicacionID { get; set; }
        public int UsuarioID { get; set; }
        public string Contenido { get; set; }
    }
}
