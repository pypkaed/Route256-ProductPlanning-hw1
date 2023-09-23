using ProductPlanningApplication.Extensions;
using ProductPlanningDataAccess.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ProductPlanningPresentation;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddApplication();
        builder.Services.AddDatabase(options 
            => options.UseInMemoryDatabase(Config.DbName));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}