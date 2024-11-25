using RedSocialWebApp.Core.Domain.Common;


namespace RedSocialWebApp.Core.Domain.Entities
{
    public class Amistad : AuditableBaseEntity
    {
        public int UsuarioID { get; set; }
        public int AmigoID { get; set; }

        // Relaciones
        public Usuario Usuario { get; set; }
        public Usuario Amigo { get; set; }
    }
}
