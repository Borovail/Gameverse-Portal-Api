using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End.Migrations
{
    /// <inheritdoc />
    public partial class UserBugReletions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Bugs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_Title",
                table: "Bugs",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_UserId",
                table: "Bugs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_AspNetUsers_UserId",
                table: "Bugs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bugs_AspNetUsers_UserId",
                table: "Bugs");

            migrationBuilder.DropIndex(
                name: "IX_Bugs_Title",
                table: "Bugs");

            migrationBuilder.DropIndex(
                name: "IX_Bugs_UserId",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bugs");
        }
    }
}
