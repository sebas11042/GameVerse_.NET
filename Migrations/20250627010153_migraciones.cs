using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameVerse.Migrations
{
    /// <inheritdoc />
    public partial class migraciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id_category = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__E548B673EF47F7E4", x => x.id_category);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    id_game = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    price_buy = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    price_rental = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Games__0E819B2CC35EB4EF", x => x.id_game);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    rol = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__D2D14637154F8C6B", x => x.id_user);
                });

            migrationBuilder.CreateTable(
                name: "Games_Category",
                columns: table => new
                {
                    id_game = table.Column<int>(type: "int", nullable: false),
                    id_category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesCategory", x => new { x.id_game, x.id_category });
                    table.ForeignKey(
                        name: "FK_GC_Category",
                        column: x => x.id_category,
                        principalTable: "Categories",
                        principalColumn: "id_category");
                    table.ForeignKey(
                        name: "FK_GC_Game",
                        column: x => x.id_game,
                        principalTable: "Games",
                        principalColumn: "id_game");
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    id_rent = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    rent_days = table.Column<int>(type: "int", nullable: true),
                    total = table.Column<int>(type: "int", nullable: true),
                    id_user = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rentals__0F4BF3B061394CDA", x => x.id_rent);
                    table.ForeignKey(
                        name: "FK_Rentals_User",
                        column: x => x.id_user,
                        principalTable: "Users",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "Shopping",
                columns: table => new
                {
                    id_buy = table.Column<int>(type: "int", nullable: false),
                    buy_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    total = table.Column<int>(type: "int", nullable: true),
                    id_user = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shopping__D507E43F2007AD30", x => x.id_buy);
                    table.ForeignKey(
                        name: "FK_Shopping_User",
                        column: x => x.id_user,
                        principalTable: "Users",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "Shopping_Cart",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false),
                    id_game = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<int>(type: "int", nullable: true),
                    AddDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => new { x.id_user, x.id_game });
                    table.ForeignKey(
                        name: "FK_SC_Game",
                        column: x => x.id_game,
                        principalTable: "Games",
                        principalColumn: "id_game");
                    table.ForeignKey(
                        name: "FK_SC_User",
                        column: x => x.id_user,
                        principalTable: "Users",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false),
                    id_game = table.Column<int>(type: "int", nullable: false),
                    added_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlist", x => new { x.id_user, x.id_game });
                    table.ForeignKey(
                        name: "FK_WL_Game",
                        column: x => x.id_game,
                        principalTable: "Games",
                        principalColumn: "id_game");
                    table.ForeignKey(
                        name: "FK_WL_User",
                        column: x => x.id_user,
                        principalTable: "Users",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "Rental_Detail",
                columns: table => new
                {
                    id_rental = table.Column<int>(type: "int", nullable: false),
                    id_game = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: true),
                    rental_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    expire_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    price = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalDetail", x => new { x.id_rental, x.id_game });
                    table.ForeignKey(
                        name: "FK_RD_Game",
                        column: x => x.id_game,
                        principalTable: "Games",
                        principalColumn: "id_game");
                    table.ForeignKey(
                        name: "FK_RD_Rental",
                        column: x => x.id_rental,
                        principalTable: "Rentals",
                        principalColumn: "id_rent");
                });

            migrationBuilder.CreateTable(
                name: "Purchase_Detail",
                columns: table => new
                {
                    id_buy = table.Column<int>(type: "int", nullable: false),
                    id_game = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => new { x.id_buy, x.id_game });
                    table.ForeignKey(
                        name: "FK_PD_Buy",
                        column: x => x.id_buy,
                        principalTable: "Shopping",
                        principalColumn: "id_buy");
                    table.ForeignKey(
                        name: "FK_PD_Game",
                        column: x => x.id_game,
                        principalTable: "Games",
                        principalColumn: "id_game");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_Category_id_category",
                table: "Games_Category",
                column: "id_category");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Detail_id_game",
                table: "Purchase_Detail",
                column: "id_game");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_Detail_id_game",
                table: "Rental_Detail",
                column: "id_game");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_id_user",
                table: "Rentals",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_Shopping_id_user",
                table: "Shopping",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_Shopping_Cart_id_game",
                table: "Shopping_Cart",
                column: "id_game");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_id_game",
                table: "Wishlist",
                column: "id_game");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games_Category");

            migrationBuilder.DropTable(
                name: "Purchase_Detail");

            migrationBuilder.DropTable(
                name: "Rental_Detail");

            migrationBuilder.DropTable(
                name: "Shopping_Cart");

            migrationBuilder.DropTable(
                name: "Wishlist");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Shopping");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
