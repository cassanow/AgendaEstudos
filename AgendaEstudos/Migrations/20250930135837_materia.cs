using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaEstudos.Migrations
{
    /// <inheritdoc />
    public partial class materia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Materia",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Materia_UserId",
                table: "Materia",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materia_User_UserId",
                table: "Materia",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materia_User_UserId",
                table: "Materia");

            migrationBuilder.DropIndex(
                name: "IX_Materia_UserId",
                table: "Materia");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Materia");
        }
    }
}
