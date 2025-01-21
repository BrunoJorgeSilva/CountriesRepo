using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CountriesApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Capital = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SubRegion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Population = table.Column<long>(type: "bigint", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    Borders = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Timezones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Languages = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
