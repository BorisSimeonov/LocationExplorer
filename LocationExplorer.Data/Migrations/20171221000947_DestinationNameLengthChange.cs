namespace LocationExplorer.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DestinationNameLengthChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Destinations",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Destinations",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);
        }
    }
}
