using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class LoginMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Students",
            //    columns: table => new
            //    {
            //        RollNo = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        FatherName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MobileNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Students", x => x.RollNo);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Teachers",
            //    columns: table => new
            //    {
            //        TeacherID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        FatherName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MobileNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Salary = table.Column<int>(type: "int", nullable: false),
            //        DOB = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Teachers", x => x.TeacherID);
            //    });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Students");

            //migrationBuilder.DropTable(
            //    name: "Teachers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
