using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobStream.Migrations
{
    /// <inheritdoc />
    public partial class ChangeJobProcessRef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobProcesses_JobBlocks_JobBlockId",
                table: "JobProcesses");

            migrationBuilder.DropIndex(
                name: "IX_JobProcesses_JobBlockId",
                table: "JobProcesses");

            migrationBuilder.DropColumn(
                name: "JobBlockId",
                table: "JobProcesses");

            migrationBuilder.AddColumn<int>(
                name: "Depth",
                table: "JobBlocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobProcessId",
                table: "JobBlocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobBlocks_JobProcessId",
                table: "JobBlocks",
                column: "JobProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobBlocks_JobProcesses_JobProcessId",
                table: "JobBlocks",
                column: "JobProcessId",
                principalTable: "JobProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobBlocks_JobProcesses_JobProcessId",
                table: "JobBlocks");

            migrationBuilder.DropIndex(
                name: "IX_JobBlocks_JobProcessId",
                table: "JobBlocks");

            migrationBuilder.DropColumn(
                name: "Depth",
                table: "JobBlocks");

            migrationBuilder.DropColumn(
                name: "JobProcessId",
                table: "JobBlocks");

            migrationBuilder.AddColumn<int>(
                name: "JobBlockId",
                table: "JobProcesses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobProcesses_JobBlockId",
                table: "JobProcesses",
                column: "JobBlockId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobProcesses_JobBlocks_JobBlockId",
                table: "JobProcesses",
                column: "JobBlockId",
                principalTable: "JobBlocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
