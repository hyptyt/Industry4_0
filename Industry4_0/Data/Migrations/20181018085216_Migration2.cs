using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Industry4_0.Data.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Heading",
                table: "EmergingTechnologiesFeedback",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                table: "EmergingTechnologiesFeedback",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmergingTechnologies",
                table: "EmergingTechnologiesFeedback",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Agree",
                table: "EmergingTechnologiesFeedback",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Disagree",
                table: "EmergingTechnologiesFeedback",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CompaniesFeedback",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Agree = table.Column<int>(nullable: false),
                    Companies = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Disagree = table.Column<int>(nullable: false),
                    Feedback = table.Column<string>(nullable: false),
                    Heading = table.Column<string>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompaniesFeedback", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompaniesFeedback");

            migrationBuilder.DropColumn(
                name: "Agree",
                table: "EmergingTechnologiesFeedback");

            migrationBuilder.DropColumn(
                name: "Disagree",
                table: "EmergingTechnologiesFeedback");

            migrationBuilder.AlterColumn<string>(
                name: "Heading",
                table: "EmergingTechnologiesFeedback",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                table: "EmergingTechnologiesFeedback",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "EmergingTechnologies",
                table: "EmergingTechnologiesFeedback",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
