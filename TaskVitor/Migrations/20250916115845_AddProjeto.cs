using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskVitor.Migrations
{
    /// <inheritdoc />
    public partial class AddProjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Tarefas",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "ProjetoId",
                table: "Tarefas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Classificacoes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "Projetos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projetos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_ProjetoId",
                table: "Tarefas",
                column: "ProjetoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Projetos_ProjetoId",
                table: "Tarefas",
                column: "ProjetoId",
                principalTable: "Projetos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Projetos_ProjetoId",
                table: "Tarefas");

            migrationBuilder.DropTable(
                name: "Projetos");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_ProjetoId",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "ProjetoId",
                table: "Tarefas");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Tarefas",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Classificacoes",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
