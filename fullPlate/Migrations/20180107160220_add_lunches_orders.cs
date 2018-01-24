using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace fullPlate.Migrations
{
    public partial class add_lunches_orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Restaurants",
                type: "bool",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Dishes",
                type: "bool",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "AspNetUsers",
                type: "bool",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "LunchDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Deleted = table.Column<bool>(type: "bool", nullable: false),
                    Enabled = table.Column<bool>(type: "bool", nullable: false, defaultValue: false),
                    LastRegisterDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    LunchDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    PaidByCompany = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LunchDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LunchDayDishes",
                columns: table => new
                {
                    DishId = table.Column<int>(type: "int4", nullable: false),
                    LunchDayId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LunchDayDishes", x => new { x.DishId, x.LunchDayId });
                    table.ForeignKey(
                        name: "FK_LunchDayDishes_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LunchDayDishes_LunchDays_LunchDayId",
                        column: x => x.LunchDayId,
                        principalTable: "LunchDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Deleted = table.Column<bool>(type: "bool", nullable: false),
                    LunchDayId = table.Column<int>(type: "int4", nullable: true),
                    LunchId = table.Column<int>(type: "int4", nullable: false),
                    MainDishId = table.Column<int>(type: "int4", nullable: true),
                    SoupId = table.Column<int>(type: "int4", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_LunchDays_LunchDayId",
                        column: x => x.LunchDayId,
                        principalTable: "LunchDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Dishes_MainDishId",
                        column: x => x.MainDishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Dishes_SoupId",
                        column: x => x.SoupId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LunchDayDishes_LunchDayId",
                table: "LunchDayDishes",
                column: "LunchDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LunchDayId",
                table: "Orders",
                column: "LunchDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MainDishId",
                table: "Orders",
                column: "MainDishId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SoupId",
                table: "Orders",
                column: "SoupId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LunchDayDishes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "LunchDays");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "AspNetUsers");
        }
    }
}
