using Microsoft.EntityFrameworkCore.Migrations;

namespace Utility.Ef.Demo.Migrations.Product
{
    public partial class a4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_brand",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    letter = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    product_catagory_id = table.Column<long>(type: "INTEGER", nullable: false),
                    orders = table.Column<int>(type: "INTEGER", nullable: false),
                    product_count = table.Column<long>(type: "INTEGER", nullable: false),
                    shop_id = table.Column<long>(type: "INTEGER", nullable: false),
                    factory_status = table.Column<bool>(type: "INTEGER", nullable: false),
                    if_show = table.Column<bool>(type: "INTEGER", nullable: false),
                    comment_count = table.Column<long>(type: "INTEGER", nullable: false),
                    logo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    images = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    logo_id = table.Column<long>(type: "INTEGER", nullable: false),
                    image_ids = table.Column<long>(type: "INTEGER", maxLength: 500, nullable: false),
                    big_pic = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    brand_story = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    tag = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_brand", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "t_cart",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    product_ids = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    user_id = table.Column<long>(type: "INTEGER", nullable: true),
                    creation_time = table.Column<long>(type: "INTEGER", nullable: false),
                    last_modification_time = table.Column<long>(type: "INTEGER", nullable: false),
                    deletion_time = table.Column<long>(type: "INTEGER", nullable: false),
                    is_deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_cart", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "t_cart_detail",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    product_id = table.Column<long>(type: "INTEGER", nullable: false),
                    number = table.Column<long>(type: "INTEGER", nullable: false),
                    user_id = table.Column<long>(type: "INTEGER", nullable: true),
                    creation_time = table.Column<long>(type: "INTEGER", nullable: false),
                    last_modification_time = table.Column<long>(type: "INTEGER", nullable: false),
                    deletion_time = table.Column<long>(type: "INTEGER", nullable: false),
                    is_deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_cart_detail", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "t_catagory_attribue",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    catagory_id = table.Column<long>(type: "INTEGER", nullable: false),
                    orders = table.Column<int>(type: "INTEGER", nullable: false),
                    parent_id = table.Column<long>(type: "INTEGER", nullable: false),
                    creation_time = table.Column<long>(type: "INTEGER", nullable: false),
                    last_modification_time = table.Column<long>(type: "INTEGER", nullable: false),
                    deletion_time = table.Column<long>(type: "INTEGER", nullable: false),
                    is_deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_catagory_attribue", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "t_product",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    catagory_id = table.Column<long>(type: "INTEGER", nullable: false),
                    sales = table.Column<int>(type: "INTEGER", nullable: false),
                    stock = table.Column<int>(type: "INTEGER", nullable: false),
                    keywords = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    score = table.Column<int>(type: "INTEGER", nullable: false),
                    create_account = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    hit = table.Column<int>(type: "INTEGER", nullable: false),
                    title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    price = table.Column<decimal>(type: "TEXT", nullable: true),
                    now_price = table.Column<decimal>(type: "TEXT", nullable: true),
                    update_account = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    activity_id = table.Column<long>(type: "INTEGER", nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false),
                    product_html = table.Column<string>(type: "TEXT", maxLength: 2147483647, nullable: true),
                    is_new = table.Column<bool>(type: "INTEGER", nullable: false),
                    introduce = table.Column<string>(type: "TEXT", maxLength: 2147483647, nullable: true),
                    search_key = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    images = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    max_picture = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "TEXT", maxLength: 2147483647, nullable: true),
                    unit = table.Column<string>(type: "TEXT", maxLength: 5, nullable: true),
                    picture = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    sale = table.Column<bool>(type: "INTEGER", nullable: false),
                    gift_id = table.Column<long>(type: "INTEGER", nullable: false),
                    creation_time = table.Column<long>(type: "INTEGER", nullable: false),
                    last_modification_time = table.Column<long>(type: "INTEGER", nullable: false),
                    deletion_time = table.Column<long>(type: "INTEGER", nullable: false),
                    is_deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "t_product_attribute",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    attribute_id = table.Column<long>(type: "INTEGER", nullable: false),
                    product_id = table.Column<long>(type: "INTEGER", nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    creation_time = table.Column<long>(type: "INTEGER", nullable: false),
                    last_modification_time = table.Column<long>(type: "INTEGER", nullable: false),
                    deletion_time = table.Column<long>(type: "INTEGER", nullable: false),
                    is_deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_product_attribute", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "t_product_catagory",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    orders = table.Column<int>(type: "INTEGER", nullable: false),
                    parent_id = table.Column<long>(type: "INTEGER", nullable: false),
                    shop_id = table.Column<long>(type: "INTEGER", nullable: false),
                    link = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    target = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    flag = table.Column<int>(type: "INTEGER", nullable: false),
                    image_id = table.Column<long>(type: "INTEGER", nullable: false),
                    creation_time = table.Column<long>(type: "INTEGER", nullable: false),
                    last_modification_time = table.Column<long>(type: "INTEGER", nullable: false),
                    deletion_time = table.Column<long>(type: "INTEGER", nullable: false),
                    is_deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_product_catagory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "t_spec",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    stock = table.Column<int>(type: "INTEGER", nullable: false),
                    sales = table.Column<int>(type: "INTEGER", nullable: false),
                    product_id = table.Column<long>(type: "INTEGER", nullable: true),
                    price = table.Column<decimal>(type: "TEXT", nullable: true),
                    now_price = table.Column<decimal>(type: "TEXT", nullable: true),
                    size = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    status = table.Column<int>(type: "INTEGER", nullable: false),
                    color = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    creation_time = table.Column<long>(type: "INTEGER", nullable: false),
                    last_modification_time = table.Column<long>(type: "INTEGER", nullable: false),
                    deletion_time = table.Column<long>(type: "INTEGER", nullable: false),
                    is_deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_spec", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_brand");

            migrationBuilder.DropTable(
                name: "t_cart");

            migrationBuilder.DropTable(
                name: "t_cart_detail");

            migrationBuilder.DropTable(
                name: "t_catagory_attribue");

            migrationBuilder.DropTable(
                name: "t_product");

            migrationBuilder.DropTable(
                name: "t_product_attribute");

            migrationBuilder.DropTable(
                name: "t_product_catagory");

            migrationBuilder.DropTable(
                name: "t_spec");
        }
    }
}
