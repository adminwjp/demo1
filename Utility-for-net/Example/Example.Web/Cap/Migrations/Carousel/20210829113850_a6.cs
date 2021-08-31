using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Example.Web.Cap.Migrations.Carousel
{
    public partial class a6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_carousel",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    image_id = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    background = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    src = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    desc = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    enable = table.Column<bool>(type: "INTEGER", nullable: false),
                    flag = table.Column<int>(type: "INTEGER", nullable: false),
                    remark = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    orders = table.Column<int>(type: "INTEGER", nullable: false),
                    link = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    creation_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    last_modification_time = table.Column<DateTime>(type: "TEXT", nullable: true),
                    deletion_time = table.Column<DateTime>(type: "TEXT", nullable: true),
                    is_deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_carousel", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_carousel");
        }
    }
}
