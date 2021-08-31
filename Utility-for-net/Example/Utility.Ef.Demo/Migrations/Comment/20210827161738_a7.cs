using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Utility.Ef.Demo.Migrations.Comment
{
    public partial class a7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_akka_comment",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    reply = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    order_detail_id = table.Column<long>(type: "INTEGER", maxLength: 36, nullable: false),
                    nickname = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    star = table.Column<int>(type: "INTEGER", nullable: false),
                    product_id = table.Column<long>(type: "INTEGER", maxLength: 36, nullable: false),
                    order_id = table.Column<long>(type: "INTEGER", maxLength: 36, nullable: false),
                    content = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    status = table.Column<bool>(type: "INTEGER", maxLength: 1, nullable: false),
                    account = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    creation_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    last_modification_time = table.Column<DateTime>(type: "TEXT", nullable: true),
                    deletion_time = table.Column<DateTime>(type: "TEXT", nullable: true),
                    is_deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_akka_comment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "t_akka_comment_type",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    code = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    status = table.Column<bool>(type: "INTEGER", maxLength: 1, nullable: false),
                    creation_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    last_modification_time = table.Column<DateTime>(type: "TEXT", nullable: true),
                    deletion_time = table.Column<DateTime>(type: "TEXT", nullable: true),
                    is_deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_akka_comment_type", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_akka_comment");

            migrationBuilder.DropTable(
                name: "t_akka_comment_type");
        }
    }
}
