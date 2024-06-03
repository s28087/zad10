using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        //zarejestrowanie zaleznosci
        builder.Services.AddDbContext<PharmacyDbContext>(opt =>
        {
            string connString = builder.Configuration.GetConnectionString("DefaultConnection");
            opt.UseSqlServer(connString);
        });

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }
}