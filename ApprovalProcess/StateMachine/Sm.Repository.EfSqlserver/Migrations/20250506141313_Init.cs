using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sm.Repository.EfSqlserver.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sm_ExecutableAction",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sm_ExecutableAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sm_StateMachine",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    InitialState = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Firer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sm_StateMachine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sm_StateSettings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StateMachineId = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sm_StateSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sm_StateSettings_Sm_StateMachine_StateMachineId",
                        column: x => x.StateMachineId,
                        principalTable: "Sm_StateMachine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sm_StateSettingsAction",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    StateSettingsId = table.Column<string>(type: "varchar(50)", nullable: false),
                    ExecutableActionId = table.Column<string>(type: "varchar(50)", nullable: false),
                    Configuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfigurationType = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sm_StateSettingsAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sm_StateSettingsAction_Sm_StateSettings_StateSettingsId",
                        column: x => x.StateSettingsId,
                        principalTable: "Sm_StateSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sm_Transition",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    Trigger = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DtState = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StateSettingsId = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sm_Transition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sm_Transition_Sm_StateSettings_StateSettingsId",
                        column: x => x.StateSettingsId,
                        principalTable: "Sm_StateSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sm_StateSettings_StateMachineId",
                table: "Sm_StateSettings",
                column: "StateMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Sm_StateSettingsAction_StateSettingsId",
                table: "Sm_StateSettingsAction",
                column: "StateSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Sm_Transition_StateSettingsId",
                table: "Sm_Transition",
                column: "StateSettingsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sm_ExecutableAction");

            migrationBuilder.DropTable(
                name: "Sm_StateSettingsAction");

            migrationBuilder.DropTable(
                name: "Sm_Transition");

            migrationBuilder.DropTable(
                name: "Sm_StateSettings");

            migrationBuilder.DropTable(
                name: "Sm_StateMachine");
        }
    }
}
