using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobStream.Migrations
{
    /// <inheritdoc />
    public partial class ChnageDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobResults_Jobs_JobId",
                table: "JobResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobProcesses_JobProcessId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobStreamHistories_JobProcesses_JobProcessId",
                table: "JobStreamHistories");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "JobStreamHistories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobProcessName",
                table: "JobStreamHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MockDuration",
                table: "Jobs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "MockResult",
                table: "Jobs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MockResultStatus",
                table: "Jobs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobName",
                table: "JobResults",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_JobResults_Jobs_JobId",
                table: "JobResults",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobProcesses_JobProcessId",
                table: "Jobs",
                column: "JobProcessId",
                principalTable: "JobProcesses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobStreamHistories_JobProcesses_JobProcessId",
                table: "JobStreamHistories",
                column: "JobProcessId",
                principalTable: "JobProcesses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobResults_Jobs_JobId",
                table: "JobResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobProcesses_JobProcessId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobStreamHistories_JobProcesses_JobProcessId",
                table: "JobStreamHistories");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "JobStreamHistories");

            migrationBuilder.DropColumn(
                name: "JobProcessName",
                table: "JobStreamHistories");

            migrationBuilder.DropColumn(
                name: "MockDuration",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "MockResult",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "MockResultStatus",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobName",
                table: "JobResults");

            migrationBuilder.AddForeignKey(
                name: "FK_JobResults_Jobs_JobId",
                table: "JobResults",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobProcesses_JobProcessId",
                table: "Jobs",
                column: "JobProcessId",
                principalTable: "JobProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobStreamHistories_JobProcesses_JobProcessId",
                table: "JobStreamHistories",
                column: "JobProcessId",
                principalTable: "JobProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
