using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeLogger.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Freelancers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Freelancers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    FreelancerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Deadline = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeRegistrations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DateOfWork = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeRegistrations_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Freelancers",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Freelancer 1" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "FreelancerId", "Name" },
                values: new object[] { 1, 1, "Customer 1" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CustomerId", "Deadline", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 5, 11, 20, 51, 5, 633, DateTimeKind.Local).AddTicks(8056), "Test Project 1 Description", "Test Project 1" },
                    { 2, 1, new DateTime(2020, 6, 7, 20, 51, 5, 638, DateTimeKind.Local).AddTicks(2328), "Test Project 2 Description", "Test Project 2" },
                    { 3, 1, new DateTime(2020, 5, 6, 20, 51, 5, 638, DateTimeKind.Local).AddTicks(2525), "Test Project 3 Description", "Test Project 3" },
                    { 4, 1, new DateTime(2020, 5, 18, 20, 51, 5, 638, DateTimeKind.Local).AddTicks(2546), "Test Project 4 Description", "Test Project 4" }
                });

            migrationBuilder.InsertData(
                table: "TimeRegistrations",
                columns: new[] { "Id", "DateOfWork", "Description", "Duration", "ProjectId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2020, 5, 8, 18, 51, 5, 638, DateTimeKind.Local).AddTicks(4856), "Write unit tests", new TimeSpan(0, 2, 30, 0, 0), 1 },
                    { 2L, new DateTime(2020, 5, 8, 14, 51, 5, 638, DateTimeKind.Local).AddTicks(8010), "Change framework version", new TimeSpan(0, 5, 30, 0, 0), 1 },
                    { 3L, new DateTime(2020, 5, 8, 8, 51, 5, 638, DateTimeKind.Local).AddTicks(8125), "test descrption 2", new TimeSpan(0, 0, 30, 0, 0), 2 },
                    { 4L, new DateTime(2020, 4, 23, 20, 51, 5, 638, DateTimeKind.Local).AddTicks(8139), "create initial structure", new TimeSpan(0, 2, 30, 0, 0), 3 },
                    { 5L, new DateTime(2020, 4, 24, 20, 51, 5, 638, DateTimeKind.Local).AddTicks(8151), "design DB", new TimeSpan(0, 1, 30, 0, 0), 3 },
                    { 6L, new DateTime(2020, 4, 26, 20, 51, 5, 638, DateTimeKind.Local).AddTicks(8161), "add business logic", new TimeSpan(0, 2, 30, 0, 0), 3 },
                    { 7L, new DateTime(2020, 4, 30, 20, 51, 5, 638, DateTimeKind.Local).AddTicks(8171), "tests", new TimeSpan(0, 4, 30, 0, 0), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_FreelancerId",
                table: "Customers",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistrations_ProjectId",
                table: "TimeRegistrations",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeRegistrations");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Freelancers");
        }
    }
}
