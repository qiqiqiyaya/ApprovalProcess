using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ap.Repository.EfSqlserver.Migrations
{
    /// <inheritdoc />
    public partial class inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ap_Employee",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ap_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ap_Organization",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ap_Organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ap_TriggeredRecord",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    StateMachineId = table.Column<string>(type: "varchar(50)", nullable: false),
                    CurrentState = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ap_TriggeredRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ap_Manager",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    EmployeeId = table.Column<string>(type: "varchar(50)", nullable: false),
                    OrganizationId = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ap_Manager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ap_Manager_Ap_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Ap_Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ap_NextApprover",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    ApproverId = table.Column<string>(type: "varchar(50)", nullable: false),
                    Definition = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TriggeredRecordId = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ap_NextApprover", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ap_NextApprover_Ap_TriggeredRecord_TriggeredRecordId",
                        column: x => x.TriggeredRecordId,
                        principalTable: "Ap_TriggeredRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ap_Manager_OrganizationId",
                table: "Ap_Manager",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Ap_NextApprover_TriggeredRecordId",
                table: "Ap_NextApprover",
                column: "TriggeredRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ap_Employee");

            migrationBuilder.DropTable(
                name: "Ap_Manager");

            migrationBuilder.DropTable(
                name: "Ap_NextApprover");

            migrationBuilder.DropTable(
                name: "Ap_Organization");

            migrationBuilder.DropTable(
                name: "Ap_TriggeredRecord");
        }
    }
}
