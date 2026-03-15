using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    /// <inheritdoc />
    public partial class DBcontext : Migration
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
                    table.PrimaryKey("PK__Authors__70DAFC146E049A97", x => x.AuthorID);
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
                    table.PrimaryKey("PK__Categori__19093A2B11B09F43", x => x.CategoryID);
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
                    table.PrimaryKey("PK__Roles__8AFACE3AFF4FD502", x => x.RoleID);
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
                    table.PrimaryKey("PK__Books__3DE0C227DCD6DF1A", x => x.BookID);
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
                    table.PrimaryKey("PK__Users__1788CCAC8D7913B3", x => x.UserID);
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
                    table.PrimaryKey("PK__BookAuth__6AED6DE6B5937920", x => new { x.BookID, x.AuthorID });
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
                    table.PrimaryKey("PK__BookCate__9C705185A3AC1CD6", x => new { x.BookID, x.CategoryID });
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
                    table.PrimaryKey("PK__BookCopi__C26CCCE50337D64C", x => x.CopyID);
                    table.ForeignKey(
                        name: "FK_BookCopies_Books",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    ReaderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CardNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MembershipDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Status = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Readers__8E67A58161E77ED4", x => x.ReaderID);
                    table.ForeignKey(
                        name: "FK_Readers_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    LoanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CopyID = table.Column<int>(type: "int", nullable: false),
                    BookTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReaderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoanDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    ReaderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Loans__4F5AD43757D5E2B7", x => x.LoanID);
                    table.ForeignKey(
                        name: "FK_Loans_BookCopies",
                        column: x => x.CopyID,
                        principalTable: "BookCopies",
                        principalColumn: "CopyID");
                    table.ForeignKey(
                        name: "FK_Loans_Readers_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Readers",
                        principalColumn: "ReaderID");
                    table.ForeignKey(
                        name: "FK_Loans_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "ReaderComments",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReaderID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ReaderCo__C3B4DFAAF70E10BE", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_ReaderComments_Books",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID");
                    table.ForeignKey(
                        name: "FK_ReaderComments_Readers",
                        column: x => x.ReaderID,
                        principalTable: "Readers",
                        principalColumn: "ReaderID");
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
                    table.PrimaryKey("PK__Fines__9D4A9BCC9DB4A575", x => x.FineID);
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
                name: "UQ__BookCopi__177800D305EC2023",
                table: "BookCopies",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryID",
                table: "Books",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "UQ__Books__447D36EAF96F4030",
                table: "Books",
                column: "ISBN",
                unique: true,
                filter: "[ISBN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Categori__8517B2E01FDA9B6A",
                table: "Categories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Fines__4F5AD436A99F718A",
                table: "Fines",
                column: "LoanID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CopyID",
                table: "Loans",
                column: "CopyID");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_ReaderId",
                table: "Loans",
                column: "ReaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_UserID",
                table: "Loans",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReaderComments_BookID",
                table: "ReaderComments",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_ReaderComments_ReaderID",
                table: "ReaderComments",
                column: "ReaderID");

            migrationBuilder.CreateIndex(
                name: "IX_Readers_UserID",
                table: "Readers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__Readers__A4E9FFE9D0906913",
                table: "Readers",
                column: "CardNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Roles__8A2B6160AD07F913",
                table: "Roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534E52D3018",
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
                name: "ReaderComments");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "BookCopies");

            migrationBuilder.DropTable(
                name: "Readers");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
