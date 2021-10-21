using Blauhaus.BLE.Abstractions.BluetoothSensor;
using Blauhaus.BLE.BluetoothSensors;
using Microsoft.Extensions.DependencyInjection;

namespace Blauhaus.BLE.Ioc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBluetooth(this IServiceCollection services)
        {
            services.AddSingleton<IBluetoothSensor, BluetoothSensor>();

            return services;
        }
    }
}