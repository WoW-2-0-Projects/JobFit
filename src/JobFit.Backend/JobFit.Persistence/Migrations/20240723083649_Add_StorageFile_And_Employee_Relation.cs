using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFit.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_StorageFile_And_Employee_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StorageFiles_OwnerId",
                table: "StorageFiles",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_StorageFiles_Users_OwnerId",
                table: "StorageFiles",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageFiles_Users_OwnerId",
                table: "StorageFiles");

            migrationBuilder.DropIndex(
                name: "IX_StorageFiles_OwnerId",
                table: "StorageFiles");
        }
    }
}
