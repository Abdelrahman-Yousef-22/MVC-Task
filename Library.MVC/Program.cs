using Library.BL;
using Library.DAL;
using Microsoft.EntityFrameworkCore;

namespace Library.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Services


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<LibraryContext>(options=>
            options.UseSqlServer("Server=DESKTOP-85Q5KQD\\SS17;Database=ELocalizeLibraryDB;Trusted_Connection=true;Encrypt=false"));

            #region Repos

            builder.Services.AddScoped<IBooksRepo, BooksRepo>();
            builder.Services.AddScoped<IClientsRepo, ClientsRepo>();
            builder.Services.AddScoped<IBorrowedBooksRepo, BorrowedBooksRepo>();
            builder.Services.AddScoped<IReturnedBooksRepo, ReturnedBooksRepo>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            #region Managers

            builder.Services.AddScoped<IClientsManager, ClientsManager>();
            builder.Services.AddScoped<IBooksManager, BooksManager>();



            #endregion

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Books}/{action=Index}/{id?}");

            app.Run();
        }
    }
}