using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RantPaw.DataContext.Migrations
{
    public partial class AddReactionsToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ref");

            migrationBuilder.CreateTable(
                name: "Reactions",
                schema: "ref",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "ref",
                table: "Reactions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "like" });

            migrationBuilder.InsertData(
                schema: "ref",
                table: "Reactions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "dislike" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reactions",
                schema: "ref");
        }
    }
}
