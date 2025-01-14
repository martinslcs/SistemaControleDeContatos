using Microsoft.EntityFrameworkCore;
using SistemaControleDeContatos.Data;
using SistemaControleDeContatos.Repositorio;

namespace SistemaControleDeContatos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adicionar servi�os
            builder.Services.AddControllersWithViews();

            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<BancoContext>(o =>
                    o.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));
            builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();

            var app = builder.Build();

            // Configurar pipeline de requisi��o HTTP
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
        }
    }
}
