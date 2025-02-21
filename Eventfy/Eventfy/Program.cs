using Eventfy.Data;
using Eventfy.Interface;
using Eventfy.Interface.Interface_Services;
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

            // Configuração de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost", policy =>
                {
                    policy.WithOrigins("http://localhost:3000") 
                          .AllowAnyHeader()  
                          .AllowAnyMethod(); 
                });
            });
            // Add services to the container.

            builder.Services.AddControllers();

           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IEventPersist, EventPersist>();
            builder.Services.AddScoped<EventService>();
            builder.Services.AddScoped<IEventServices, EventService>();
            builder.Services.AddScoped<ILocalPersist, LocalPersist>();
            builder.Services.AddScoped<LocalService>();
            builder.Services.AddScoped<IParticipantPersist, ParticipantPersist>();
            builder.Services.AddScoped<ParticipantService>();
            builder.Services.AddScoped<IEventParticipantPersist, EventParticipantPersist>();
            builder.Services.AddScoped<IParticipantService, ParticipantService>();
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