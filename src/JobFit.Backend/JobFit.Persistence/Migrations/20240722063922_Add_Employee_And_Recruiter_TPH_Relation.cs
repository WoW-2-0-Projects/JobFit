using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFit.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Employee_And_Recruiter_TPH_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Users",
                newName: "UserType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "Users",
                newName: "Discriminator");
        }
    }
}
