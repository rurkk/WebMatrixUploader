using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMatrixUploader.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColumnNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    XColumnName = table.Column<string>(type: "TEXT", nullable: true),
                    YColumnName = table.Column<string>(type: "TEXT", nullable: true),
                    AppUserFileFileId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColumnNames_Files_AppUserFileFileId",
                        column: x => x.AppUserFileFileId,
                        principalTable: "Files",
                        principalColumn: "FileId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColumnNames_AppUserFileFileId",
                table: "ColumnNames",
                column: "AppUserFileFileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColumnNames");
        }
    }
}
