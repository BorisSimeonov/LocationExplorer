namespace LocationExplorer.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ArticleAuthorIdSetToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_AuthorId1",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_AuthorId1",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Articles",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_AuthorId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Articles",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "AuthorId1",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorId1",
                table: "Articles",
                column: "AuthorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_AuthorId1",
                table: "Articles",
                column: "AuthorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
