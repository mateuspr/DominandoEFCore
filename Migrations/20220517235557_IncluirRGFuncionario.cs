using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.Migrations
{
    public partial class IncluirRGFuncionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_funcionarios_Departamentos_DepartamentoId",
                table: "funcionarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_funcionarios",
                table: "funcionarios");

            migrationBuilder.RenameTable(
                name: "funcionarios",
                newName: "Funcionarios");

            migrationBuilder.RenameIndex(
                name: "IX_funcionarios_DepartamentoId",
                table: "Funcionarios",
                newName: "IX_Funcionarios_DepartamentoId");

            migrationBuilder.AddColumn<string>(
                name: "RG",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcionarios",
                table: "Funcionarios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Departamentos_DepartamentoId",
                table: "Funcionarios",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Departamentos_DepartamentoId",
                table: "Funcionarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcionarios",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "RG",
                table: "Funcionarios");

            migrationBuilder.RenameTable(
                name: "Funcionarios",
                newName: "funcionarios");

            migrationBuilder.RenameIndex(
                name: "IX_Funcionarios_DepartamentoId",
                table: "funcionarios",
                newName: "IX_funcionarios_DepartamentoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_funcionarios",
                table: "funcionarios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_funcionarios_Departamentos_DepartamentoId",
                table: "funcionarios",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
