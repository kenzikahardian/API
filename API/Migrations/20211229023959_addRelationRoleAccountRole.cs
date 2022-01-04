using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addRelationRoleAccountRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNIK",
                table: "tb_tr_accountRole",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_accountRole_AccountNIK",
                table: "tb_tr_accountRole",
                column: "AccountNIK");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_accountRole_RoleID",
                table: "tb_tr_accountRole",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_accountRole_tb_m_account_AccountNIK",
                table: "tb_tr_accountRole",
                column: "AccountNIK",
                principalTable: "tb_m_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_accountRole_tb_m_role_RoleID",
                table: "tb_tr_accountRole",
                column: "RoleID",
                principalTable: "tb_m_role",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_accountRole_tb_m_account_AccountNIK",
                table: "tb_tr_accountRole");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_accountRole_tb_m_role_RoleID",
                table: "tb_tr_accountRole");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_accountRole_AccountNIK",
                table: "tb_tr_accountRole");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_accountRole_RoleID",
                table: "tb_tr_accountRole");

            migrationBuilder.DropColumn(
                name: "AccountNIK",
                table: "tb_tr_accountRole");
        }
    }
}
