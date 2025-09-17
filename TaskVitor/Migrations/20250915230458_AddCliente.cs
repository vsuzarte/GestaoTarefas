using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskVitor.Migrations
{
    /// <inheritdoc />
    public partial class AddCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Tarefas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_ClienteId",
                table: "Tarefas",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Clientes_ClienteId",
                table: "Tarefas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Clientes_ClienteId",
                table: "Tarefas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_ClienteId",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Tarefas");
        }
    }
}
