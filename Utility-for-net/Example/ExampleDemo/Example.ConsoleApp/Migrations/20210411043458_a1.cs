using Microsoft.EntityFrameworkCore.Migrations;

namespace Example.Migrations
{
    public partial class a1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Num = table.Column<int>(nullable: false),
                    Des = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Account = table.Column<string>(nullable: true),
                    Pwd = table.Column<string>(nullable: true),
                    RegIp = table.Column<long>(nullable: false),
                    RegDate = table.Column<long>(nullable: false),
                    ModifyDate = table.Column<long>(nullable: false),
                    LoginDate = table.Column<long>(nullable: false),
                    LoginFailCount = table.Column<long>(nullable: false),
                    LoginIp = table.Column<long>(nullable: false),
                    TimeSpan = table.Column<long>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    RegIp = table.Column<long>(nullable: false),
                    OldPwd = table.Column<long>(nullable: false),
                    NewPwd = table.Column<long>(nullable: false),
                    CreateDate = table.Column<long>(nullable: false),
                    Flag = table.Column<long>(nullable: false),
                    ModifyDate = table.Column<long>(nullable: false),
                    LoginDate = table.Column<long>(nullable: false),
                    LoginIp = table.Column<long>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_UserId",
                table: "UserLogs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "UserLogs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
