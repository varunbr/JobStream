using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobStream.Migrations
{
    /// <inheritdoc />
    public partial class ChnageBlockType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobBlockType",
                table: "JobBlocks",
                newName: "BlockType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlockType",
                table: "JobBlocks",
                newName: "JobBlockType");
        }
    }
}
