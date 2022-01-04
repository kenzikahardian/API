using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addNewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "tb_m_role");

            migrationBuilder.RenameTable(
                name: "AccountRoles",
                newName: "tb_tr_accountRole");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_role",
                table: "tb_m_role",
                column: "RoleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_tr_accountRole",
                table: "tb_tr_accountRole",
                column: "AccountRoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_tr_accountRole",
                table: "tb_tr_accountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_role",
                table: "tb_m_role");

            migrationBuilder.RenameTable(
                name: "tb_tr_accountRole",
                newName: "AccountRoles");

            migrationBuilder.RenameTable(
                name: "tb_m_role",
                newName: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles",
                column: "AccountRoleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "RoleID");
        }
    }
}
