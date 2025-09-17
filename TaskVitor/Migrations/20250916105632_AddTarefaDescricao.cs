using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskVitor.Migrations
{
    /// <inheritdoc />
    public partial class AddTarefaDescricao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Tarefas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Tarefas");
        }
    }
}
