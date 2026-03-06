using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class DBContext1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Authors__70DAFC14A134266B", x => x.AuthorID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__19093A2B21FFB7DC", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__8AFACE3A059DD22A", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ISBN = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    PublishedYear = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Books__3DE0C22786223BD3", x => x.BookID);
                    table.ForeignKey(
                        name: "FK_Books_Categories",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCACE5103981", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Roles",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookAuth__6AED6DE626BC2D8C", x => new { x.BookID, x.AuthorID });
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "AuthorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCategories",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookCate__9C705185A8E6F9AF", x => new { x.BookID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK_BookCategories_Books",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCategories_Categories",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCopies",
                columns: table => new
                {
                    CopyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    Barcode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookCopi__C26CCCE52251282C", x => x.CopyID);
                    table.ForeignKey(
                        name: "FK_BookCopies_Books",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    LoanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CopyID = table.Column<int>(type: "int", nullable: false),
                    LoanDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Loans__4F5AD437E7A21FAA", x => x.LoanID);
                    table.ForeignKey(
                        name: "FK_Loans_BookCopies",
                        column: x => x.CopyID,
                        principalTable: "BookCopies",
                        principalColumn: "CopyID");
                    table.ForeignKey(
                        name: "FK_Loans_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Fines",
                columns: table => new
                {
                    FineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FineDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsPaid = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Fines__9D4A9BCC7B1A05C9", x => x.FineID);
                    table.ForeignKey(
                        name: "FK_Fines_Loans",
                        column: x => x.LoanID,
                        principalTable: "Loans",
                        principalColumn: "LoanID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_AuthorID",
                table: "BookAuthors",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_CategoryID",
                table: "BookCategories",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_BookID",
                table: "BookCopies",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "UQ__BookCopi__177800D37282507B",
                table: "BookCopies",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryID",
                table: "Books",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "UQ__Books__447D36EA8A2D2C15",
                table: "Books",
                column: "ISBN",
                unique: true,
                filter: "[ISBN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Categori__8517B2E01F1EB48F",
                table: "Categories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Fines__4F5AD436819E6677",
                table: "Fines",
                column: "LoanID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CopyID",
                table: "Loans",
                column: "CopyID");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_UserID",
                table: "Loans",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__Roles__8A2B61602E0D6944",
                table: "Roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534F8C7987C",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "BookCategories");

            migrationBuilder.DropTable(
                name: "Fines");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "BookCopies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
