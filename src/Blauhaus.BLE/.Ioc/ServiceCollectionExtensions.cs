using Microsoft.Extensions.DependencyInjection;

namespace Blauhaus.BLE.Ioc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBluetooth(this IServiceCollection services)
        {

            return services;
        }
    }
}