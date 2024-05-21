using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MoodSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MoodEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MoodSchedules_UserId",
                table: "MoodSchedules",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MoodEntries_UserId",
                table: "MoodEntries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoodEntries_Users_UserId",
                table: "MoodEntries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoodSchedules_Users_UserId",
                table: "MoodSchedules",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoodEntries_Users_UserId",
                table: "MoodEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_MoodSchedules_Users_UserId",
                table: "MoodSchedules");

            migrationBuilder.DropIndex(
                name: "IX_MoodSchedules_UserId",
                table: "MoodSchedules");

            migrationBuilder.DropIndex(
                name: "IX_MoodEntries_UserId",
                table: "MoodEntries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MoodSchedules");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MoodEntries");
        }
    }
}
