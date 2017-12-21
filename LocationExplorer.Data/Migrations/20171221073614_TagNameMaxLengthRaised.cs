namespace LocationExplorer.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class TagNameMaxLengthRaised : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
