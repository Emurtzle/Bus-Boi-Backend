using Microsoft.EntityFrameworkCore.Migrations;

namespace BusBoiBackend.Migrations
{
    public partial class AddedColorToWatchedRouteStops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "WatchedRouteStops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "WatchedRouteStops");
        }
    }
}
