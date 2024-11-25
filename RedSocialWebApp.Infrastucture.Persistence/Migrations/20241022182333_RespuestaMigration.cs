using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedSocialWebApp.Infrastucture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RespuestaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Respuesta",
                table: "ComentarioRespuestas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Respuesta",
                table: "ComentarioRespuestas");
        }
    }
}
