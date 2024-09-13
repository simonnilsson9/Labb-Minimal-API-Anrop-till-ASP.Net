using Web_Application.Services;

namespace Web_Application
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            // Add services to the container.
            builder.Services.AddScoped<IBookService, BookService>();
			builder.Services.AddHttpClient();

			//Adding the URL
			StaticDetails.BookApiBase = builder.Configuration["ServiceUrls:LabbWebAPI"];

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
				pattern: "{controller=Book}/{action=Start}/{id?}");

			app.Run();
		}
	}
}
