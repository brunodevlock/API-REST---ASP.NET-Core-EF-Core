using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Data;
using ProductCatalog.Repositories;

namespace ProductCatalog
{
    public class Startup
    {
        //Aqui se adiciona o MiddleWere - Validações e services entre o Request/Response
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Cria um item apenas 
            // AddScoped verifica se existe algum Datacontext na memória e se não existir 
            // ele me dá um novo!
            services.AddScoped<StoreDataContext, StoreDataContext>();

            // É add transiente pq toda vez que eu pedir um ProductRepository eu quero uma nova instância 
            // do ProductRepository
            services.AddTransient<ProductRepository, ProductRepository>();
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

                app.UseMvc();
        }   
    }
}

