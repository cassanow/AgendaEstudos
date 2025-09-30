using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaEstudos.Migrations
{
    /// <inheritdoc />
    public partial class corrigindo_dnv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Prioridade",
                table: "Tarefa",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prioridade",
                table: "Tarefa");
        }
    }
}
