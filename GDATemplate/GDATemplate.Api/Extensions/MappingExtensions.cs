namespace GDATemplate.Api.Extensions
{
    public static class MappingExtensions
    {
        public static void AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }


}
