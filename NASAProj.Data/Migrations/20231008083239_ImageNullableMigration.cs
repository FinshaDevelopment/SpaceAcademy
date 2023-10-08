using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NASAProj.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImageNullableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Attachments_ImageId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Attachments_ImageId",
                table: "Users",
                column: "ImageId",
                principalTable: "Attachments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Attachments_ImageId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Attachments_ImageId",
                table: "Users",
                column: "ImageId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
