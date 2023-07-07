using DsDelivery.WebApi.Configuration;
using DsDelivery.Manager.Mapping;

namespace DsDelivery.WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllers();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        services.AddDataBaseConfiguration(Configuration);
        services.AddDependencyInjectionConfiguration();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddFluentValidationConfiguration();
        services.AddSwaggerConfiguration();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DsDeliveryApi v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors();

       

        app.UseJwtConfiguration();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
