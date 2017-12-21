namespace LocationExplorer.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangesInGalleryAndArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GalleryPeriodEnd",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "GalleryPeriodStart",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "ApprovedBySupport",
                table: "Articles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "GalleryPeriodEnd",
                table: "Galleries",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "GalleryPeriodStart",
                table: "Galleries",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedBySupport",
                table: "Articles",
                nullable: false,
                defaultValue: false);
        }
    }
}
