using Microsoft.EntityFrameworkCore.Migrations;

namespace Utility.Ef.Demo.Migrations.Company
{
    public partial class a3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_c_catagory",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    button_href1 = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    button_name1 = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    button_href2 = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    button_name2 = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    flag = table.Column<int>(type: "INTEGER", nullable: false),
                    body = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    review = table.Column<int>(type: "INTEGER", nullable: false),
                    color = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    process = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    style = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    background_image = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    href = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    feature = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    icon = table.Column<string>(type: "TEXT", nullable: true),
                    filter = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    parent_id = table.Column<long>(type: "INTEGER", maxLength: 50, nullable: true),
                    tel = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    logo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    logo1 = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    lanage = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    create_date = table.Column<long>(type: "INTEGER", nullable: false),
                    modify_date = table.Column<long>(type: "INTEGER", nullable: false),
                    enable = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_c_catagory_t_c_catagory_parent_id",
                        column: x => x.parent_id,
                        principalTable: "t_c_catagory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_c_lange",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    val1 = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    val2 = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    val3 = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    val4 = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    val5 = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    val6 = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    val7 = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    relation_id = table.Column<long>(type: "INTEGER", nullable: false),
                    relation_table = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    lanage = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    create_date = table.Column<long>(type: "INTEGER", nullable: false),
                    modify_date = table.Column<long>(type: "INTEGER", nullable: false),
                    enable = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "t_c_menu",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Soure = table.Column<string>(type: "TEXT", nullable: true),
                    Orders = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationTime = table.Column<long>(type: "INTEGER", nullable: false),
                    LastModificationTime = table.Column<long>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletionTime = table.Column<long>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    State = table.Column<string>(type: "TEXT", maxLength: 6, nullable: true),
                    Checked = table.Column<bool>(type: "INTEGER", nullable: false),
                    AttributesJson = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    IconCls = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Collpse = table.Column<bool>(type: "INTEGER", nullable: false),
                    Groups = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Icon = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Href = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    HuiIcon = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IdName = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    AceIcon = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    parent_id = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_c_menu", x => x.Id);
                    table.ForeignKey(
                        name: "fk_parent_id",
                        column: x => x.parent_id,
                        principalTable: "t_c_menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_c_relation",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fk1 = table.Column<long>(type: "INTEGER", nullable: false),
                    fk2 = table.Column<long>(type: "INTEGER", nullable: false),
                    flag = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    lanage = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    create_date = table.Column<long>(type: "INTEGER", nullable: false),
                    modify_date = table.Column<long>(type: "INTEGER", nullable: false),
                    enable = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_c_catagory_parent_id",
                table: "t_c_catagory",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_c_menu_parent_id",
                table: "t_c_menu",
                column: "parent_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_c_catagory");

            migrationBuilder.DropTable(
                name: "t_c_lange");

            migrationBuilder.DropTable(
                name: "t_c_menu");

            migrationBuilder.DropTable(
                name: "t_c_relation");
        }
    }
}
