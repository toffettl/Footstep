using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Footstep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialmigrationinpointofinterest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Traces");

            migrationBuilder.CreateTable(
                name: "PointOfInterests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ExpireAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Like = table.Column<int>(type: "integer", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    PointOfInterestType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointOfInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointOfInterests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PointOfInterests_UserId",
                table: "PointOfInterests",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointOfInterests");

            migrationBuilder.CreateTable(
                name: "Traces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ExpireAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Like = table.Column<int>(type: "integer", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Traces_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Traces_UserId",
                table: "Traces",
                column: "UserId");
        }
    }
}
