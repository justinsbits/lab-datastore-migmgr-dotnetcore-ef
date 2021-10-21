using Microsoft.EntityFrameworkCore.Migrations;

namespace CommanderDBMigrationMgr.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HowTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommandLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commands_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tools",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "", "dotnet" });

            migrationBuilder.InsertData(
                table: "Tools",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "", "docker" });

            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "CommandLine", "HowTo", "ToolId" },
                values: new object[] { 1, "dotnet user-secrets init", "Enable secret storage for project", 1 });

            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "CommandLine", "HowTo", "ToolId" },
                values: new object[] { 2, "dotnet user-secrets set \"<key>\" \"<value>\"", "Set secret for project", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Commands_ToolId",
                table: "Commands",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_Name",
                table: "Tools",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "Tools");
        }
    }
}
