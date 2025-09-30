using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaEstudos.Migrations
{
    /// <inheritdoc />
    public partial class corrigindo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materia_Tarefa_TarefaId1",
                table: "Materia");

            migrationBuilder.DropIndex(
                name: "IX_Materia_TarefaId1",
                table: "Materia");

            migrationBuilder.DropColumn(
                name: "MateriaId",
                table: "Tarefa");

            migrationBuilder.DropColumn(
                name: "TarefaId1",
                table: "Materia");

            migrationBuilder.CreateIndex(
                name: "IX_Materia_TarefaId",
                table: "Materia",
                column: "TarefaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materia_Tarefa_TarefaId",
                table: "Materia",
                column: "TarefaId",
                principalTable: "Tarefa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materia_Tarefa_TarefaId",
                table: "Materia");

            migrationBuilder.DropIndex(
                name: "IX_Materia_TarefaId",
                table: "Materia");

            migrationBuilder.AddColumn<int>(
                name: "MateriaId",
                table: "Tarefa",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TarefaId1",
                table: "Materia",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materia_TarefaId1",
                table: "Materia",
                column: "TarefaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Materia_Tarefa_TarefaId1",
                table: "Materia",
                column: "TarefaId1",
                principalTable: "Tarefa",
                principalColumn: "Id");
        }
    }
}
