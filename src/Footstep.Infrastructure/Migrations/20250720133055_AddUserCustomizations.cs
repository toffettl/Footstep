using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Footstep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCustomizations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcessoryStyle",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AvatarOverProfile",
                table: "Users",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BagStyle",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "HeadStyle",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegStyle",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapStyle",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PointOfInterestStyle",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TorsoStyle",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnlockedAcessoryStyles",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnlockedBagStyles",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnlockedHeadStyles",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnlockedLegStyles",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnlockedMapStyles",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnlockedPointOfInterestStyles",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnlockedTorsoStyles",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcessoryStyle",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AvatarOverProfile",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BagStyle",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HeadStyle",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LegStyle",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MapStyle",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PointOfInterestStyle",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TorsoStyle",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UnlockedAcessoryStyles",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UnlockedBagStyles",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UnlockedHeadStyles",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UnlockedLegStyles",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UnlockedMapStyles",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UnlockedPointOfInterestStyles",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UnlockedTorsoStyles",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Users");
        }
    }
}
