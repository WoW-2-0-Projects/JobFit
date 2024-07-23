using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFit.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Change_Employee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTemporaryFile",
                table: "StorageFiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTemporaryFile",
                table: "StorageFiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
