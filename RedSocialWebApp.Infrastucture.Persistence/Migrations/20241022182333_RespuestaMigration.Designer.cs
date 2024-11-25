﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RedSocialWebApp.Infrastructure.Persistence.Contexts;

#nullable disable

namespace RedSocialWebApp.Infrastucture.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20241022182333_RespuestaMigration")]
    partial class RespuestaMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RedSocialWebApp.Core.Domain.Entities.Amistad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AmigoID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AmigoID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Amistades", (string)null);
                });

            modelBuilder.Entity("RedSocialWebApp.Core.Domain.Entities.Comentario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PublicacionID")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PublicacionID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Comentarios", (string)null);
                });

            modelBuilder.Entity("RedSocialWebApp.Core.Domain.Entities.ComentarioRespuesta", b =>
                {
                    b.Property<int>("ComentarioPrincipalId")
                        .HasColumnType("int");

                    b.Property<int>("ComentarioRespuestaId")
                        .HasColumnType("int");

                    b.Property<int?>("ComentarioId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Respuesta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.HasKey("ComentarioPrincipalId", "ComentarioRespuestaId");

                    b.HasIndex("ComentarioId");

                    b.HasIndex("ComentarioRespuestaId");

                    b.HasIndex("UsuarioID");

                    b.ToTable("ComentarioRespuestas", (string)null);
                });

            modelBuilder.Entity("RedSocialWebApp.Core.Domain.Entities.Publicacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imagen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.Property<string>("VideoYouTube")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Publicaciones", (string)null);
                });

            modelBuilder.Entity("RedSocialWebApp.Core.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FotoPerfil")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("RedSocialWebApp.Core.Domain.Entities.Amistad", b =>
                {
                    b.HasOne("RedSocialWebApp.Core.Domain.Entities.Usuario", "Amigo")
                        .WithMany()
                        .HasForeignKey("AmigoID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RedSocialWebApp.Core.Domain.Entities.Usuario", "Usuario")
                        .WithMany("Amistades")
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Amigo");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("RedSocialWebApp.Core.Domain.Entities.Comentario", b =>
                {
                    b.HasOne("RedSocialWebApp.Core.Domain.Entities.Publicacion", "Publicacion")
                        .WithMany("Comentarios")
                        .HasForeignKey("PublicacionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RedSocialWebApp.Core.Domain.Entities.Usuario", "Usuario")
                        .WithMany("Comentarios")
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Publicacion");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("RedSocialWebApp.Core.Domain.Entities.ComentarioRespuesta", b =>
                {
                    b.HasOne("RedSocialWebApp.Core.Domain.Entities.Comentario", null)
                        .WithMany("Respuestas")
                        .HasForeignKey("ComentarioId");

                    b.HasOne("RedSocialWebApp.Core.Domain.Entities.Comentario", "ComentarioPrincipal")
                        .WithMany("ComentariosPrincipales")
                        .HasForeignKey("ComentarioPrincipalId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RedSocialWebApp.Core.Domain.Entities.Comentario", "ComentarioRelacionado")
                        .WithMany()
                        .HasForeignKey("ComentarioRespuestaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RedSocialWebApp.Core.Domain.Entities.Usuario", "Usuario")
                        .WithMany("Respuestas")
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ComentarioPrincipal");

                    b.Navigation("ComentarioRelacionado");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("RedSocialWebApp.Core.Domain.Entities.Publicacion", b =>
                {
                    b.HasOne("RedSocialWebApp.Core.Domain.Entities.Usuario", "Usuario")
                        .WithMany("Publicaciones")
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("RedSocialWebApp.Core.Domain.Entities.Comentario", b =>
                {
                    b.Navigation("ComentariosPrincipales");

                    b.Navigation("Respuestas");
                });

            modelBuilder.Entity("RedSocialWebApp.Core.Domain.Entities.Publicacion", b =>
                {
                    b.Navigation("Comentarios");
                });

            modelBuilder.Entity("RedSocialWebApp.Core.Domain.Entities.Usuario", b =>
                {
                    b.Navigation("Amistades");

                    b.Navigation("Comentarios");

                    b.Navigation("Publicaciones");

                    b.Navigation("Respuestas");
                });
#pragma warning restore 612, 618
        }
    }
}
