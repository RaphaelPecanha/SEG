using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEG.Migrations
{
    /// <inheritdoc />
    public partial class TestIdUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id_usuario",
                table: "usuarios",
                newName: "id_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id_user",
                table: "usuarios",
                newName: "id_usuario");
        }
    }
}
