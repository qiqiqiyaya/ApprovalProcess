using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ap.Repository.EfSqlserver.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ap_ExecutableAction",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ap_ExecutableAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ap_StateMachine",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    InitialState = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CurrentState = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ap_StateMachine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ap_StateSettings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StateMachineId = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ap_StateSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ap_StateSettings_Ap_StateMachine_StateMachineId",
                        column: x => x.StateMachineId,
                        principalTable: "Ap_StateMachine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ap_StateSettingsAction",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    StateSettingsId = table.Column<string>(type: "varchar(50)", nullable: false),
                    ExecutableActionId = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ap_StateSettingsAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ap_StateSettingsAction_Ap_StateSettings_StateSettingsId",
                        column: x => x.StateSettingsId,
                        principalTable: "Ap_StateSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ap_Transition",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    Trigger = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DtState = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StateSettingsId = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ap_Transition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ap_Transition_Ap_StateSettings_StateSettingsId",
                        column: x => x.StateSettingsId,
                        principalTable: "Ap_StateSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ap_StateSettings_StateMachineId",
                table: "Ap_StateSettings",
                column: "StateMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Ap_StateSettingsAction_StateSettingsId",
                table: "Ap_StateSettingsAction",
                column: "StateSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Ap_Transition_StateSettingsId",
                table: "Ap_Transition",
                column: "StateSettingsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ap_ExecutableAction");

            migrationBuilder.DropTable(
                name: "Ap_StateSettingsAction");

            migrationBuilder.DropTable(
                name: "Ap_Transition");

            migrationBuilder.DropTable(
                name: "Ap_StateSettings");

            migrationBuilder.DropTable(
                name: "Ap_StateMachine");
        }
    }
}
