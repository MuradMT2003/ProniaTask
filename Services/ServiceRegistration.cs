using ProniaTask.ExtensionServices.Implements;
using ProniaTask.ExtensionServices.Interfaces;
using ProniaTask.Services.Implements;
using ProniaTask.Services.Interfaces;

namespace ProniaTask.Services
{
    public static class ServiceRegistration
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<LayoutService>();
        }
    }
}
