using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiT.Scheduler.Data.PostgreSql.Migrations
{
    public partial class AddingAllTenantRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Songs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Performers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Genres",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Songs_TenantId",
                table: "Songs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Performers_TenantId",
                table: "Performers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_TenantId",
                table: "Genres",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Tenants_TenantId",
                table: "Genres",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Performers_Tenants_TenantId",
                table: "Performers",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Tenants_TenantId",
                table: "Songs",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Tenants_TenantId",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Performers_Tenants_TenantId",
                table: "Performers");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Tenants_TenantId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_TenantId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Performers_TenantId",
                table: "Performers");

            migrationBuilder.DropIndex(
                name: "IX_Genres_TenantId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Performers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Genres");
        }
    }
}
