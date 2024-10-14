using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class addNewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todoMusicas_TodoUsers_UserId",
                table: "todoMusicas");

            migrationBuilder.DropIndex(
                name: "IX_todoMusicas_UserId",
                table: "todoMusicas");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "todoMusicas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "todoMusicas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_todoMusicas_UserId",
                table: "todoMusicas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_todoMusicas_TodoUsers_UserId",
                table: "todoMusicas",
                column: "UserId",
                principalTable: "TodoUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
