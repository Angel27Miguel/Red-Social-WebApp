using Microsoft.EntityFrameworkCore;
using RedSocialWebApp.Core.Domain.Common;
using RedSocialWebApp.Core.Domain.Entities;

namespace RedSocialWebApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<ComentarioRespuesta> ComentarioRespuestas { get; set; }
        public DbSet<Amistad> Amistades { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API

            #region "Tablas"

            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Publicacion>().ToTable("Publicaciones");
            modelBuilder.Entity<Comentario>().ToTable("Comentarios");
            modelBuilder.Entity<Amistad>().ToTable("Amistades");
            modelBuilder.Entity<ComentarioRespuesta>().ToTable("ComentarioRespuestas"); 

            #endregion

            #region "Llaves primarias"

            modelBuilder.Entity<Usuario>().HasKey(usuario => usuario.Id);
            modelBuilder.Entity<Publicacion>().HasKey(publicacion => publicacion.Id);
            modelBuilder.Entity<Comentario>().HasKey(comentario => comentario.Id);
            modelBuilder.Entity<Amistad>().HasKey(amistad => amistad.Id);

            // Configuracion de llave compuesta para ComentarioRespuesta
            modelBuilder.Entity<ComentarioRespuesta>()
                .HasKey(cr => new { cr.ComentarioPrincipalId, cr.ComentarioRespuestaId }); 

            #endregion

            #region "Relaciones"

            // Usuario -> Publicacion
            modelBuilder.Entity<Publicacion>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Publicaciones)
                .HasForeignKey(p => p.UsuarioID)
                .OnDelete(DeleteBehavior.Cascade); 

            // Usuario -> Comentario
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Comentarios)
                .HasForeignKey(c => c.UsuarioID)
                .OnDelete(DeleteBehavior.Restrict); 

            // Publicacion -> Comentario
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Publicacion)
                .WithMany(p => p.Comentarios)
                .HasForeignKey(c => c.PublicacionID)
                .OnDelete(DeleteBehavior.Cascade); 

            // Configuracion para ComentarioRespuesta
            modelBuilder.Entity<ComentarioRespuesta>()
                .HasOne(cr => cr.ComentarioPrincipal)
                .WithMany(c => c.ComentariosPrincipales) 
                .HasForeignKey(cr => cr.ComentarioPrincipalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComentarioRespuesta>()
                .HasOne(cr => cr.ComentarioRelacionado)
                .WithMany(c => c.Respuestas) 
                .HasForeignKey(cr => cr.ComentarioRespuestaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComentarioRespuesta>()
                .HasOne(cr => cr.ComentarioRelacionado)
                .WithMany()
                .HasForeignKey(cr => cr.ComentarioRespuestaId)
                .OnDelete(DeleteBehavior.Restrict); 

            // Usuario -> Amistad (Usuario es el que solicita la amistad)
            modelBuilder.Entity<Amistad>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Amistades)
                .HasForeignKey(a => a.UsuarioID)
                .OnDelete(DeleteBehavior.Restrict); 

            // Usuario -> Amistad (Amigo es el usuario amigo)
            modelBuilder.Entity<Amistad>()
                .HasOne(a => a.Amigo)
                .WithMany()
                .HasForeignKey(a => a.AmigoID)
                .OnDelete(DeleteBehavior.Restrict);

            // Usuario -> ComentarioRespuesta
            modelBuilder.Entity<ComentarioRespuesta>()
                .HasOne(cr => cr.Usuario)
                .WithMany(u => u.Respuestas) 
                .HasForeignKey(cr => cr.UsuarioID)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }
}
