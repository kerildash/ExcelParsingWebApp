using ExcelParsingWebApp.Database;
using ExcelParsingWebApp.Database.Repositories;
using ExcelParsingWebApp.Models.Domain;
using ExcelParsingWebApp.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ExcelParsingWebApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IWorksheetReader, WorksheetReader>();
            builder.Services.AddScoped<IFileService, FileService>();

            builder.Services.AddScoped<AccountRepository>();
            builder.Services.AddScoped<ClassRepository>();
            builder.Services.AddScoped<SheetRepository>();
			builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
			builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllersWithViews();

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
				pattern: "{controller=Sheet}/{action=Index}");

			app.Run();
		}
	}
}
