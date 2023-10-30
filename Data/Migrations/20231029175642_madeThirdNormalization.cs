using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMatrixUploader.Data.Migrations
{
    /// <inheritdoc />
    public partial class madeThirdNormalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColumnNames_Files_AppUserFileFileId",
                table: "ColumnNames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ColumnNames",
                table: "ColumnNames");

            migrationBuilder.DropIndex(
                name: "IX_ColumnNames_AppUserFileFileId",
                table: "ColumnNames");

            migrationBuilder.DropColumn(
                name: "AppUserFileFileId",
                table: "ColumnNames");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "Files",
                newName: "AppUserFileId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ColumnNames",
                newName: "AppUserFileId");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserFileId",
                table: "ColumnNames",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "PairOfXyColumnsId",
                table: "ColumnNames",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ColumnNames",
                table: "ColumnNames",
                column: "PairOfXyColumnsId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColumnNames_Files_AppUserFileId",
                table: "ColumnNames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ColumnNames",
                table: "ColumnNames");

            migrationBuilder.DropIndex(
                name: "IX_ColumnNames_AppUserFileId",
                table: "ColumnNames");

            migrationBuilder.DropColumn(
                name: "PairOfXyColumnsId",
                table: "ColumnNames");

            migrationBuilder.RenameColumn(
                name: "AppUserFileId",
                table: "Files",
                newName: "FileId");

            migrationBuilder.RenameColumn(
                name: "AppUserFileId",
                table: "ColumnNames",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ColumnNames",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "AppUserFileFileId",
                table: "ColumnNames",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ColumnNames",
                table: "ColumnNames",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ColumnNames_AppUserFileFileId",
                table: "ColumnNames",
                column: "AppUserFileFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColumnNames_Files_AppUserFileFileId",
                table: "ColumnNames",
                column: "AppUserFileFileId",
                principalTable: "Files",
                principalColumn: "FileId");
        }
    }
}
