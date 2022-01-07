using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp102.Data.Migrations
{
    public partial class Initializer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblPersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPersons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "tblPersons",
                columns: new[] { "Id", "Name", "Surname" },
                values: new object[] { 1, "Semih ", "Semih " });

            migrationBuilder.InsertData(
                table: "tblPersons",
                columns: new[] { "Id", "Name", "Surname" },
                values: new object[] { 2, "Ali ", "Osman " });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPersons");
        }
    }
}
