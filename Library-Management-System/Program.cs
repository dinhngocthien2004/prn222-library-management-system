
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

            // ================== BẮT ĐẦU CẤU HÌNH DỊCH VỤ ==================

            // 1. Thêm dịch vụ MVC (Controllers và Views)
            builder.Services.AddControllersWithViews();

            // 2. Đọc chuỗi kết nối
            var connectionString = builder.Configuration.GetConnectionString("LibraryConn");

            // 3. Đăng ký Entity Framework Core (Kết nối SQL)       
            builder.Services.AddDbContext<LibraryManagementDbContext>(options =>
            options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("LibraryManagementSystem")));

            // 4. Đăng ký các lớp DAL và BLL (Dependency Injection)
            // Mỗi khi tạo thêm Repository hay Service mới, bạn phải khai báo thêm ở đây
            //builder.Services.AddScoped<BookRepository>();
            //builder.Services.AddScoped<BookService>();

            builder.Services.AddScoped<BookDAO>();
            builder.Services.AddScoped<CategoryDAO>();
            builder.Services.AddScoped<LoanDAO>();
            builder.Services.AddScoped<BookCopyDAO>();
            builder.Services.AddScoped<UserDAO>();

            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ILoanRepository, LoanRepository>();
            builder.Services.AddScoped<IBookCopyRepository, BookCopyRepository>();


            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ILoanService, LoanService>();
            builder.Services.AddScoped<IBookCopyService, BookCopyService>();

            // ================== KẾT THÚC CẤU HÌNH DỊCH VỤ ==================

            var app = builder.Build(); // <-- Dòng này chốt sổ, không viết code đăng ký dịch vụ sau dòng này

            // Cấu hình HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
            //+++++++++++++++++++++++++++++++
            //var builder = WebApplication.CreateBuilder(args);

            //// Add services to the container.
            //builder.Services.AddControllersWithViews();

            //var app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            //app.Run();
        }
    }
}
