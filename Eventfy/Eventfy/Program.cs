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

            // Configura��o de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost", policy =>
                {
                    policy.WithOrigins("http://localhost:3000") // Permite requisi��es de localhost:3000 (onde o React geralmente roda)
                          .AllowAnyHeader()  // Permite qualquer cabe�alho
                          .AllowAnyMethod(); // Permite qualquer m�todo HTTP (GET, POST, etc)
                });
            });
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
            builder.Services.AddScoped<IEventParticipantPersist, EventParticipantPersist>();
            builder.Services.AddScoped<EventParticipantServices>();
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });


            var app = builder.Build();
            app.UseCors("AllowLocalhost");


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