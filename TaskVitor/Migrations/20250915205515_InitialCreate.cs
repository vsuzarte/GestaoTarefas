using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskVitor.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classificacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classificacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClassificacaoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarefas_Classificacoes_ClassificacaoId",
                        column: x => x.ClassificacaoId,
                        principalTable: "Classificacoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_ClassificacaoId",
                table: "Tarefas",
                column: "ClassificacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarefas");

            migrationBuilder.DropTable(
                name: "Classificacoes");
        }
    }
}
