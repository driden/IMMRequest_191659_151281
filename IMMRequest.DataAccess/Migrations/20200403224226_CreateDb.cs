using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IMMRequest.DataAccess.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Citizens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestAreas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestTopics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    RequestAreaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestTopics_RequestAreas_RequestAreaId",
                        column: x => x.RequestAreaId,
                        principalTable: "RequestAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(nullable: true),
                    RequestTopicId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestTypes_RequestTopics_RequestTopicId",
                        column: x => x.RequestTopicId,
                        principalTable: "RequestTopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    IsRequired = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    RequestTypeId = table.Column<int>(nullable: true),
                    Type = table.Column<DateTime>(nullable: true),
                    IntegerField_Type = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Field_RequestTypes_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalTable: "RequestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item<DateTime>",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldId = table.Column<int>(nullable: false),
                    Value = table.Column<DateTime>(nullable: false),
                    DateFieldId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item<DateTime>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item<DateTime>_Field_DateFieldId",
                        column: x => x.DateFieldId,
                        principalTable: "Field",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item<int>",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldId = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    IntegerFieldId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item<int>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item<int>_Field_IntegerFieldId",
                        column: x => x.IntegerFieldId,
                        principalTable: "Field",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Field_RequestTypeId",
                table: "Field",
                column: "RequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Item<DateTime>_DateFieldId",
                table: "Item<DateTime>",
                column: "DateFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Item<int>_IntegerFieldId",
                table: "Item<int>",
                column: "IntegerFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTopics_RequestAreaId",
                table: "RequestTopics",
                column: "RequestAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTypes_RequestTopicId",
                table: "RequestTypes",
                column: "RequestTopicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citizens");

            migrationBuilder.DropTable(
                name: "Item<DateTime>");

            migrationBuilder.DropTable(
                name: "Item<int>");

            migrationBuilder.DropTable(
                name: "Field");

            migrationBuilder.DropTable(
                name: "RequestTypes");

            migrationBuilder.DropTable(
                name: "RequestTopics");

            migrationBuilder.DropTable(
                name: "RequestAreas");
        }
    }
}
