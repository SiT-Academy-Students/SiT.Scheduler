using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiT.Scheduler.Data.PostgreSql.Migrations
{
    public partial class RefactoringTheIdentityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                table: "Identities",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Identities_ExternalId",
                table: "Identities",
                column: "ExternalId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Identities_ExternalId",
                table: "Identities");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Identities");
        }
    }
}
