using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanLoom_B.Migrations
{
    /// <inheritdoc />
    public partial class UrbanLoomMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_user_UserId",
                table: "cart");

            migrationBuilder.DropForeignKey(
                name: "FK_cartitems_cart_CartId",
                table: "cartitems");

            migrationBuilder.DropForeignKey(
                name: "FK_cartitems_products_ProductId",
                table: "cartitems");

            migrationBuilder.DropForeignKey(
                name: "FK_orderitems_orders_OrderId",
                table: "orderitems");

            migrationBuilder.DropForeignKey(
                name: "FK_orderitems_products_ProductId",
                table: "orderitems");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_user_userId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_whishlist_products_ProductId",
                table: "whishlist");

            migrationBuilder.DropForeignKey(
                name: "FK_whishlist_user_UserId",
                table: "whishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_whishlist",
                table: "whishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orders",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orderitems",
                table: "orderitems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cartitems",
                table: "cartitems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cart",
                table: "cart");

            migrationBuilder.RenameTable(
                name: "whishlist",
                newName: "Whishlist_ul");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "Users_ul");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "Products_ul");

            migrationBuilder.RenameTable(
                name: "orders",
                newName: "Orders_ul");

            migrationBuilder.RenameTable(
                name: "orderitems",
                newName: "Orderitems_ul");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories_ul");

            migrationBuilder.RenameTable(
                name: "cartitems",
                newName: "Cartitem_ul");

            migrationBuilder.RenameTable(
                name: "cart",
                newName: "Cart_ul");

            migrationBuilder.RenameIndex(
                name: "IX_whishlist_UserId",
                table: "Whishlist_ul",
                newName: "IX_Whishlist_ul_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_whishlist_ProductId",
                table: "Whishlist_ul",
                newName: "IX_Whishlist_ul_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_products_CategoryId",
                table: "Products_ul",
                newName: "IX_Products_ul_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_orders_userId",
                table: "Orders_ul",
                newName: "IX_Orders_ul_userId");

            migrationBuilder.RenameIndex(
                name: "IX_orderitems_ProductId",
                table: "Orderitems_ul",
                newName: "IX_Orderitems_ul_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_orderitems_OrderId",
                table: "Orderitems_ul",
                newName: "IX_Orderitems_ul_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_cartitems_ProductId",
                table: "Cartitem_ul",
                newName: "IX_Cartitem_ul_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_cartitems_CartId",
                table: "Cartitem_ul",
                newName: "IX_Cartitem_ul_CartId");

            migrationBuilder.RenameIndex(
                name: "IX_cart_UserId",
                table: "Cart_ul",
                newName: "IX_Cart_ul_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Whishlist_ul",
                table: "Whishlist_ul",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users_ul",
                table: "Users_ul",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products_ul",
                table: "Products_ul",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders_ul",
                table: "Orders_ul",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orderitems_ul",
                table: "Orderitems_ul",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories_ul",
                table: "Categories_ul",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cartitem_ul",
                table: "Cartitem_ul",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart_ul",
                table: "Cart_ul",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_ul_Users_ul_UserId",
                table: "Cart_ul",
                column: "UserId",
                principalTable: "Users_ul",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cartitem_ul_Cart_ul_CartId",
                table: "Cartitem_ul",
                column: "CartId",
                principalTable: "Cart_ul",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cartitem_ul_Products_ul_ProductId",
                table: "Cartitem_ul",
                column: "ProductId",
                principalTable: "Products_ul",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orderitems_ul_Orders_ul_OrderId",
                table: "Orderitems_ul",
                column: "OrderId",
                principalTable: "Orders_ul",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orderitems_ul_Products_ul_ProductId",
                table: "Orderitems_ul",
                column: "ProductId",
                principalTable: "Products_ul",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ul_Users_ul_userId",
                table: "Orders_ul",
                column: "userId",
                principalTable: "Users_ul",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ul_Categories_ul_CategoryId",
                table: "Products_ul",
                column: "CategoryId",
                principalTable: "Categories_ul",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Whishlist_ul_Products_ul_ProductId",
                table: "Whishlist_ul",
                column: "ProductId",
                principalTable: "Products_ul",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Whishlist_ul_Users_ul_UserId",
                table: "Whishlist_ul",
                column: "UserId",
                principalTable: "Users_ul",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_ul_Users_ul_UserId",
                table: "Cart_ul");

            migrationBuilder.DropForeignKey(
                name: "FK_Cartitem_ul_Cart_ul_CartId",
                table: "Cartitem_ul");

            migrationBuilder.DropForeignKey(
                name: "FK_Cartitem_ul_Products_ul_ProductId",
                table: "Cartitem_ul");

            migrationBuilder.DropForeignKey(
                name: "FK_Orderitems_ul_Orders_ul_OrderId",
                table: "Orderitems_ul");

            migrationBuilder.DropForeignKey(
                name: "FK_Orderitems_ul_Products_ul_ProductId",
                table: "Orderitems_ul");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ul_Users_ul_userId",
                table: "Orders_ul");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ul_Categories_ul_CategoryId",
                table: "Products_ul");

            migrationBuilder.DropForeignKey(
                name: "FK_Whishlist_ul_Products_ul_ProductId",
                table: "Whishlist_ul");

            migrationBuilder.DropForeignKey(
                name: "FK_Whishlist_ul_Users_ul_UserId",
                table: "Whishlist_ul");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Whishlist_ul",
                table: "Whishlist_ul");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users_ul",
                table: "Users_ul");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products_ul",
                table: "Products_ul");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders_ul",
                table: "Orders_ul");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orderitems_ul",
                table: "Orderitems_ul");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories_ul",
                table: "Categories_ul");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cartitem_ul",
                table: "Cartitem_ul");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart_ul",
                table: "Cart_ul");

            migrationBuilder.RenameTable(
                name: "Whishlist_ul",
                newName: "whishlist");

            migrationBuilder.RenameTable(
                name: "Users_ul",
                newName: "user");

            migrationBuilder.RenameTable(
                name: "Products_ul",
                newName: "products");

            migrationBuilder.RenameTable(
                name: "Orders_ul",
                newName: "orders");

            migrationBuilder.RenameTable(
                name: "Orderitems_ul",
                newName: "orderitems");

            migrationBuilder.RenameTable(
                name: "Categories_ul",
                newName: "categories");

            migrationBuilder.RenameTable(
                name: "Cartitem_ul",
                newName: "cartitems");

            migrationBuilder.RenameTable(
                name: "Cart_ul",
                newName: "cart");

            migrationBuilder.RenameIndex(
                name: "IX_Whishlist_ul_UserId",
                table: "whishlist",
                newName: "IX_whishlist_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Whishlist_ul_ProductId",
                table: "whishlist",
                newName: "IX_whishlist_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ul_CategoryId",
                table: "products",
                newName: "IX_products_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ul_userId",
                table: "orders",
                newName: "IX_orders_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Orderitems_ul_ProductId",
                table: "orderitems",
                newName: "IX_orderitems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Orderitems_ul_OrderId",
                table: "orderitems",
                newName: "IX_orderitems_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Cartitem_ul_ProductId",
                table: "cartitems",
                newName: "IX_cartitems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Cartitem_ul_CartId",
                table: "cartitems",
                newName: "IX_cartitems_CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_ul_UserId",
                table: "cart",
                newName: "IX_cart_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_whishlist",
                table: "whishlist",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orders",
                table: "orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orderitems",
                table: "orderitems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cartitems",
                table: "cartitems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cart",
                table: "cart",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_user_UserId",
                table: "cart",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cartitems_cart_CartId",
                table: "cartitems",
                column: "CartId",
                principalTable: "cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cartitems_products_ProductId",
                table: "cartitems",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderitems_orders_OrderId",
                table: "orderitems",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderitems_products_ProductId",
                table: "orderitems",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_user_userId",
                table: "orders",
                column: "userId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_whishlist_products_ProductId",
                table: "whishlist",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_whishlist_user_UserId",
                table: "whishlist",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
