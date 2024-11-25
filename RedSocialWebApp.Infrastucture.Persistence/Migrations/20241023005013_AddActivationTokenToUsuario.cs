using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedSocialWebApp.Infrastucture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddActivationTokenToUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivationToken",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationToken",
                table: "Usuarios");
        }
    }
}
