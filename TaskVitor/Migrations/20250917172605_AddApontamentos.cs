using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskVitor.Migrations
{
    /// <inheritdoc />
    public partial class AddApontamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Classificacoes_ClassificacaoId",
                table: "Tarefas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Clientes_ClienteId",
                table: "Tarefas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Projetos_ProjetoId",
                table: "Tarefas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Responsaveis_ResponsavelId",
                table: "Tarefas");

            migrationBuilder.AlterColumn<int>(
                name: "ResponsavelId",
                table: "Tarefas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjetoId",
                table: "Tarefas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Tarefas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClassificacaoId",
                table: "Tarefas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Apontamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TarefaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Inicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Fim = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DuracaoManual = table.Column<TimeSpan>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apontamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apontamentos_Tarefas_TarefaId",
                        column: x => x.TarefaId,
                        principalTable: "Tarefas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apontamentos_TarefaId",
                table: "Apontamentos",
                column: "TarefaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Classificacoes_ClassificacaoId",
                table: "Tarefas",
                column: "ClassificacaoId",
                principalTable: "Classificacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Clientes_ClienteId",
                table: "Tarefas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Projetos_ProjetoId",
                table: "Tarefas",
                column: "ProjetoId",
                principalTable: "Projetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Responsaveis_ResponsavelId",
                table: "Tarefas",
                column: "ResponsavelId",
                principalTable: "Responsaveis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Classificacoes_ClassificacaoId",
                table: "Tarefas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Clientes_ClienteId",
                table: "Tarefas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Projetos_ProjetoId",
                table: "Tarefas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Responsaveis_ResponsavelId",
                table: "Tarefas");

            migrationBuilder.DropTable(
                name: "Apontamentos");

            migrationBuilder.AlterColumn<int>(
                name: "ResponsavelId",
                table: "Tarefas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ProjetoId",
                table: "Tarefas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Tarefas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ClassificacaoId",
                table: "Tarefas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Classificacoes_ClassificacaoId",
                table: "Tarefas",
                column: "ClassificacaoId",
                principalTable: "Classificacoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Clientes_ClienteId",
                table: "Tarefas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Projetos_ProjetoId",
                table: "Tarefas",
                column: "ProjetoId",
                principalTable: "Projetos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Responsaveis_ResponsavelId",
                table: "Tarefas",
                column: "ResponsavelId",
                principalTable: "Responsaveis",
                principalColumn: "Id");
        }
    }
}
