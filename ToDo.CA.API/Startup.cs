using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDo.CA.Core.Interfaces;
using ToDo.CA.Core.Services.ToDoUseCases;
using ToDo.CA.Infrastructure;
using ToDo.CA.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;

namespace ToDo.CA.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {                                                                                                                                             
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("ToDoDb"));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddMediatR(typeof(CreateToDoHandler));

            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDos.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDos.API");
                c.RoutePrefix = "docs";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
