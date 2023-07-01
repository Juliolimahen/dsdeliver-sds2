using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DsDeliveryApi.Migrations
{
    /// <inheritdoc />
    public partial class FixPositionsDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Positions_PositionId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Users_UserLogin",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_UserLogin",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PositionId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserLogin",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Customers");

            migrationBuilder.CreateTable(
                name: "PositionUser",
                columns: table => new
                {
                    PositionsId = table.Column<int>(type: "int", nullable: false),
                    UsersLogin = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionUser", x => new { x.PositionsId, x.UsersLogin });
                    table.ForeignKey(
                        name: "FK_PositionUser_Positions_PositionsId",
                        column: x => x.PositionsId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PositionUser_Users_UsersLogin",
                        column: x => x.UsersLogin,
                        principalTable: "Users",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PositionUser_UsersLogin",
                table: "PositionUser",
                column: "UsersLogin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PositionUser");

            migrationBuilder.AddColumn<string>(
                name: "UserLogin",
                table: "Positions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_UserLogin",
                table: "Positions",
                column: "UserLogin");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PositionId",
                table: "Customers",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Positions_PositionId",
                table: "Customers",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Users_UserLogin",
                table: "Positions",
                column: "UserLogin",
                principalTable: "Users",
                principalColumn: "Login");
        }
    }
}
