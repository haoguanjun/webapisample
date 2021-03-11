using Microsoft.Extensions.DependencyInjection;

namespace lovepdf
{
    public static class ServicesExtensions
    {
        public static void ConfigureCore(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                );
            });
        }
    }
}