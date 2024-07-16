using AutoMapper;

namespace ChatAppAPI.Helpers
{
    public static class Extensions
    {
        public static void AddAutoMappers(this IServiceCollection services)
        {
            var appMappingConfig = new MapperConfiguration(x =>
            {
                x.AddMaps(new string[] { "ChatAppMappers" });
            });

            var appMapper = appMappingConfig.CreateMapper();
            services.AddSingleton(appMapper);
        }
    }
}
