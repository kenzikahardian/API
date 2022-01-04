using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addModelUniversityAndEducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_employees",
                table: "tb_m_employees");

            migrationBuilder.RenameTable(
                name: "tb_m_employees",
                newName: "tb_m_employee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_employee",
                table: "tb_m_employee",
                column: "NIK");

            migrationBuilder.CreateTable(
                name: "tb_m_university",
                columns: table => new
                {
                    UniversityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniversityName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_university", x => x.UniversityID);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_education",
                columns: table => new
                {
                    EducationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<int>(type: "int", nullable: false),
                    GPA = table.Column<int>(type: "int", nullable: false),
                    UniversityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_education", x => x.EducationID);
                    table.ForeignKey(
                        name: "FK_tb_m_education_tb_m_university_UniversityID",
                        column: x => x.UniversityID,
                        principalTable: "tb_m_university",
                        principalColumn: "UniversityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_education_UniversityID",
                table: "tb_m_education",
                column: "UniversityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_education");

            migrationBuilder.DropTable(
                name: "tb_m_university");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_employee",
                table: "tb_m_employee");

            migrationBuilder.RenameTable(
                name: "tb_m_employee",
                newName: "tb_m_employees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_employees",
                table: "tb_m_employees",
                column: "NIK");
        }
    }
}
