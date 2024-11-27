using Eventfy.Data;
using Eventfy.Interface;
using Eventfy.Persistence;
using Eventfy.Service;
using Microsoft.EntityFrameworkCore;

namespace Eventfy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ConnectionContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IEventPersist, EventPersist>();
            builder.Services.AddScoped<EventService>();
            builder.Services.AddScoped<ILocalPersist, LocalPersist>();
            builder.Services.AddScoped<LocalService>();
            builder.Services.AddScoped<IParticipantPersist, ParticipantPersist>();
            builder.Services.AddScoped<ParticipantService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
}