using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyNews.Core.Migrations
{
    public partial class adduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    Password = table.Column<string>(maxLength: 200, nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
