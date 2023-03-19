using HumanOrMutant.Application;
using HumanOrMutant.Persistence;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Mutant;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        AddSwager(services);
        services.AddPersistenceRepository();
        services.AddApplicationServices();
        services.AddControllers();
    }

    private void AddSwager(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Is mutant or human API",
                Contact = new OpenApiContact
                {
                    Name = "Sergio castro",
                    Email = "secastro06@gmail.com",
                    Url = new Uri("https://github.com/sergiocastrogu/HumanOrMutant")
                }
            });
            //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        string server = "/Prod";
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            server = "";
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"{server}/swagger/v1/swagger.json", "Is mutant or human API");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to Is mutant or human API");
            });
        });
    }
}