using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMatrixUploader.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColumnNames_Files_AppUserFileId",
                table: "ColumnNames");

            migrationBuilder.DropIndex(
                name: "IX_ColumnNames_AppUserFileId",
                table: "ColumnNames");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ColumnNames_AppUserFileId",
                table: "ColumnNames",
                column: "AppUserFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColumnNames_Files_AppUserFileId",
                table: "ColumnNames",
                column: "AppUserFileId",
                principalTable: "Files",
                principalColumn: "AppUserFileId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
