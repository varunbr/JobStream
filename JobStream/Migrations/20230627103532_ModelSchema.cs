using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobStream.Migrations
{
    /// <inheritdoc />
    public partial class ModelSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobBlockId",
                table: "JobProcesses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "JobBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IfBlockId = table.Column<int>(type: "integer", nullable: true),
                    ConditionBlockId = table.Column<int>(type: "integer", nullable: true),
                    ElseBlockId = table.Column<int>(type: "integer", nullable: true),
                    JobBlockType = table.Column<string>(type: "text", nullable: false),
                    ExecutionType = table.Column<string>(type: "text", nullable: true),
                    ExecutionResultType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobBlocks_JobBlocks_ConditionBlockId",
                        column: x => x.ConditionBlockId,
                        principalTable: "JobBlocks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobBlocks_JobBlocks_ElseBlockId",
                        column: x => x.ElseBlockId,
                        principalTable: "JobBlocks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobBlocks_JobBlocks_IfBlockId",
                        column: x => x.IfBlockId,
                        principalTable: "JobBlocks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobStreamHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobProcessId = table.Column<int>(type: "integer", nullable: false),
                    Added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Started = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Finished = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStreamHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobStreamHistories_JobProcesses_JobProcessId",
                        column: x => x.JobProcessId,
                        principalTable: "JobProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    JobProcessId = table.Column<int>(type: "integer", nullable: false),
                    JobBlockId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_JobBlocks_JobBlockId",
                        column: x => x.JobBlockId,
                        principalTable: "JobBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_JobProcesses_JobProcessId",
                        column: x => x.JobProcessId,
                        principalTable: "JobProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobProcessId = table.Column<int>(type: "integer", nullable: false),
                    RunId = table.Column<int>(type: "integer", nullable: false),
                    JobId = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    RunStatus = table.Column<string>(type: "text", nullable: false),
                    ResultStatus = table.Column<string>(type: "text", nullable: true),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: true),
                    Started = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Ended = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobResults_JobProcesses_JobProcessId",
                        column: x => x.JobProcessId,
                        principalTable: "JobProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobResults_JobStreamHistories_RunId",
                        column: x => x.RunId,
                        principalTable: "JobStreamHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobResults_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobProcesses_JobBlockId",
                table: "JobProcesses",
                column: "JobBlockId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobBlocks_ConditionBlockId",
                table: "JobBlocks",
                column: "ConditionBlockId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobBlocks_ElseBlockId",
                table: "JobBlocks",
                column: "ElseBlockId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobBlocks_IfBlockId",
                table: "JobBlocks",
                column: "IfBlockId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobResults_JobId",
                table: "JobResults",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobResults_JobProcessId",
                table: "JobResults",
                column: "JobProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_JobResults_RunId",
                table: "JobResults",
                column: "RunId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobBlockId",
                table: "Jobs",
                column: "JobBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobProcessId",
                table: "Jobs",
                column: "JobProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_JobStreamHistories_JobProcessId",
                table: "JobStreamHistories",
                column: "JobProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobProcesses_JobBlocks_JobBlockId",
                table: "JobProcesses",
                column: "JobBlockId",
                principalTable: "JobBlocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobProcesses_JobBlocks_JobBlockId",
                table: "JobProcesses");

            migrationBuilder.DropTable(
                name: "JobResults");

            migrationBuilder.DropTable(
                name: "JobStreamHistories");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "JobBlocks");

            migrationBuilder.DropIndex(
                name: "IX_JobProcesses_JobBlockId",
                table: "JobProcesses");

            migrationBuilder.DropColumn(
                name: "JobBlockId",
                table: "JobProcesses");
        }
    }
}
