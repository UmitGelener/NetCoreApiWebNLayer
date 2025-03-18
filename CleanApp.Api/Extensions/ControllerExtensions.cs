using CleanApp.Api.Filters;

namespace CleanApp.Api.Extensions
{
    public static class ControllerExtensions
    {
        public static IServiceCollection AddControllersWithFiltersExt(this IServiceCollection services)
        {

            services.AddScoped(typeof(NotFoundFilter<,>));

            services.AddControllers(options =>
            {
                options.Filters.Add<FluentValidatorFilter>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });
            return services;
        }
    }
}
