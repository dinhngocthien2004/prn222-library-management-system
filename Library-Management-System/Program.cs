using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;

namespace Library_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ================== SERVICES ==================

            builder.Services.AddControllersWithViews();

            // 🔥 THÊM CACHE (BẮT BUỘC CHO SESSION)
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // 🔥 DB
            builder.Services.AddDbContext<LibraryManagementDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("DataAccessObjects"))
            );

            // ================== DAO ==================
            builder.Services.AddScoped<BookDAO>();
            builder.Services.AddScoped<CategoryDAO>();
            builder.Services.AddScoped<LoanDAO>();
            builder.Services.AddScoped<BookCopyDAO>();
            builder.Services.AddScoped<AccountDAO>();
            builder.Services.AddScoped<ReaderDAO>();

            // ================== REPOSITORY ==================
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ILoanRepository, LoanRepository>();
            builder.Services.AddScoped<IBookCopyRepository, BookCopyRepository>();
            builder.Services.AddScoped<IReaderRepository, ReaderRepository>();

            // ================== SERVICE ==================
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ILoanService, LoanService>();
            builder.Services.AddScoped<IBookCopyService, BookCopyService>();
            builder.Services.AddScoped<IReaderService, ReaderService>();

            // ================== BUILD ==================
            var app = builder.Build();

            // ================== PIPELINE ==================
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // 🔥 QUAN TRỌNG: SESSION PHẢI TRƯỚC AUTH
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}