namespace UsuariosApp2.API.Configurations
{
    public class CORSConfiguration
    {
        public static string PolicyName => "DefaultPolicy";

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(cfg => cfg.AddPolicy(PolicyName, builder =>
            {
                builder.WithOrigins("http://localhost:5042/")
                       .AllowAnyHeader()
                       .AllowAnyMethod();

            }));
        }
    }
}
