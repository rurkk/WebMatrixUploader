using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMatrixUploader.Data.Migrations
{
    /// <inheritdoc />
    public partial class AbscissaAndOrdinateTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColumnNames");

            migrationBuilder.RenameColumn(
                name: "AppUserFileId",
                table: "Files",
                newName: "FileId");

            migrationBuilder.CreateTable(
                name: "Abscissa",
                columns: table => new
                {
                    AbscissaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CurveDataId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abscissa", x => x.AbscissaId);
                });

            migrationBuilder.CreateTable(
                name: "CurveData",
                columns: table => new
                {
                    CurveDataId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurveData", x => x.CurveDataId);
                });

            migrationBuilder.CreateTable(
                name: "Ordinates",
                columns: table => new
                {
                    OrdinateId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CurveDataId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordinates", x => x.OrdinateId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abscissa");

            migrationBuilder.DropTable(
                name: "CurveData");

            migrationBuilder.DropTable(
                name: "Ordinates");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "Files",
                newName: "AppUserFileId");

            migrationBuilder.CreateTable(
                name: "ColumnNames",
                columns: table => new
                {
                    PairOfXyColumnsId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppUserFileId = table.Column<int>(type: "INTEGER", nullable: false),
                    XColumnName = table.Column<string>(type: "TEXT", nullable: true),
                    YColumnName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnNames", x => x.PairOfXyColumnsId);
                });
        }
    }
}
