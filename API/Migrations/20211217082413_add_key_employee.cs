using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add_key_employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "tb_m_employees");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "tb_m_employees",
                newName: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_employees",
                table: "tb_m_employees",
                column: "NIK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_employees",
                table: "tb_m_employees");

            migrationBuilder.RenameTable(
                name: "tb_m_employees",
                newName: "Employees");

            migrationBuilder.RenameColumn(
                name: "NIK",
                table: "Employees",
                newName: "EmployeeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "EmployeeID");
        }
    }
}
