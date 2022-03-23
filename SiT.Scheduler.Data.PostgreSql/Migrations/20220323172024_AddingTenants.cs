using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiT.Scheduler.Data.PostgreSql.Migrations
{
    public partial class AddingTenants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityTenant",
                columns: table => new
                {
                    IdentitiesId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityTenant", x => new { x.IdentitiesId, x.TenantsId });
                    table.ForeignKey(
                        name: "FK_IdentityTenant_Identities_IdentitiesId",
                        column: x => x.IdentitiesId,
                        principalTable: "Identities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityTenant_Tenants_TenantsId",
                        column: x => x.TenantsId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdentityTenant_TenantsId",
                table: "IdentityTenant",
                column: "TenantsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityTenant");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
