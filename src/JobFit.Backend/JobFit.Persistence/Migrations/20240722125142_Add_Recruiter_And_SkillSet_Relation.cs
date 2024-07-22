using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFit.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Recruiter_And_SkillSet_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RecruiterId",
                table: "SkillSets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SkillSets_RecruiterId",
                table: "SkillSets",
                column: "RecruiterId");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillSets_Users_RecruiterId",
                table: "SkillSets",
                column: "RecruiterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillSets_Users_RecruiterId",
                table: "SkillSets");

            migrationBuilder.DropIndex(
                name: "IX_SkillSets_RecruiterId",
                table: "SkillSets");

            migrationBuilder.DropColumn(
                name: "RecruiterId",
                table: "SkillSets");
        }
    }
}
