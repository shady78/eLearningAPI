using eLearningAPI.Core.Consts;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection.Metadata;

#nullable disable

namespace eLearningAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
             table: "AspNetRoles",
             columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
             values: new object[] { Guid.NewGuid().ToString(), AppRoles.Admin, AppRoles.Admin.ToUpper(), Guid.NewGuid().ToString() }
             );
            migrationBuilder.InsertData(
               table: "AspNetRoles",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[] { Guid.NewGuid().ToString(), AppRoles.Teacher, AppRoles.Teacher.ToUpper(), Guid.NewGuid().ToString() }
               );
            migrationBuilder.InsertData(
               table: "AspNetRoles",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[] { Guid.NewGuid().ToString(), AppRoles.Student, AppRoles.Student.ToUpper(), Guid.NewGuid().ToString() }
               );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from [AspNetRoles]");
        }
    }
}
