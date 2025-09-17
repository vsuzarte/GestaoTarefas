using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskVitor.Migrations
{
    /// <inheritdoc />
    public partial class AddResponsavel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResponsavelId",
                table: "Tarefas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Responsaveis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsaveis", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_ResponsavelId",
                table: "Tarefas",
                column: "ResponsavelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Responsaveis_ResponsavelId",
                table: "Tarefas",
                column: "ResponsavelId",
                principalTable: "Responsaveis",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Responsaveis_ResponsavelId",
                table: "Tarefas");

            migrationBuilder.DropTable(
                name: "Responsaveis");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_ResponsavelId",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "ResponsavelId",
                table: "Tarefas");
        }
    }
}
