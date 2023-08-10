using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPSA.API.Migrations
{
    /// <inheritdoc />
    public partial class initial_project_setup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Branches",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Contactnumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FacebookUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    InstagramUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    YouTubeUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    MenuOrder = table.Column<long>(type: "bigint", nullable: true),
                    MenuLevel = table.Column<int>(type: "int", nullable: false),
                    IsLeaf = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "dbo",
                        principalTable: "Branches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleWiseMenus",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    MenuId = table.Column<long>(type: "bigint", nullable: false),
                    IsView = table.Column<bool>(type: "bit", nullable: false),
                    IsAdd = table.Column<bool>(type: "bit", nullable: false),
                    IsUpdate = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleWiseMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleWiseMenus_Menus_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "dbo",
                        principalTable: "Menus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleWiseMenus_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleWiseMenus_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleWiseMenus_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CompanyId",
                schema: "dbo",
                table: "Branches",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CreatedBy",
                schema: "dbo",
                table: "Branches",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_ModifiedBy",
                schema: "dbo",
                table: "Branches",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CreatedBy",
                schema: "dbo",
                table: "Companies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ModifiedBy",
                schema: "dbo",
                table: "Companies",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_CreatedBy",
                schema: "dbo",
                table: "Menus",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ModifiedBy",
                schema: "dbo",
                table: "Menus",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CreatedBy",
                schema: "dbo",
                table: "Roles",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ModifiedBy",
                schema: "dbo",
                table: "Roles",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RoleWiseMenus_CreatedBy",
                schema: "dbo",
                table: "RoleWiseMenus",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RoleWiseMenus_MenuId_RoleId",
                schema: "dbo",
                table: "RoleWiseMenus",
                columns: new[] { "MenuId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleWiseMenus_ModifiedBy",
                schema: "dbo",
                table: "RoleWiseMenus",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RoleWiseMenus_RoleId",
                schema: "dbo",
                table: "RoleWiseMenus",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BranchId",
                schema: "dbo",
                table: "Users",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedBy",
                schema: "dbo",
                table: "Users",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ModifiedBy",
                schema: "dbo",
                table: "Users",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                schema: "dbo",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Companies_CompanyId",
                schema: "dbo",
                table: "Branches",
                column: "CompanyId",
                principalSchema: "dbo",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Users_CreatedBy",
                schema: "dbo",
                table: "Branches",
                column: "CreatedBy",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Users_ModifiedBy",
                schema: "dbo",
                table: "Branches",
                column: "ModifiedBy",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_CreatedBy",
                schema: "dbo",
                table: "Companies",
                column: "CreatedBy",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_ModifiedBy",
                schema: "dbo",
                table: "Companies",
                column: "ModifiedBy",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Users_CreatedBy",
                schema: "dbo",
                table: "Menus",
                column: "CreatedBy",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Users_ModifiedBy",
                schema: "dbo",
                table: "Menus",
                column: "ModifiedBy",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_CreatedBy",
                schema: "dbo",
                table: "Roles",
                column: "CreatedBy",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_ModifiedBy",
                schema: "dbo",
                table: "Roles",
                column: "ModifiedBy",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Companies_CompanyId",
                schema: "dbo",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Users_CreatedBy",
                schema: "dbo",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Users_ModifiedBy",
                schema: "dbo",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_CreatedBy",
                schema: "dbo",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_ModifiedBy",
                schema: "dbo",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "RoleWiseMenus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Menus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Branches",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "dbo");
        }
    }
}
