using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Footstep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationInTrace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Traces",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Traces",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Traces");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Traces");
        }
    }
}
