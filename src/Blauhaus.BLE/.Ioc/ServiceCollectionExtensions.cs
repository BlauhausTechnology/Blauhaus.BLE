using Blauhaus.BLE.Abstractions.BluetoothSensor;
using Blauhaus.BLE.Abstractions.Devices;
using Blauhaus.BLE.BluetoothSensors;
using Blauhaus.BLE.Devices;
using Microsoft.Extensions.DependencyInjection;

namespace Blauhaus.BLE.Ioc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBluetooth(this IServiceCollection services)
        {
            services
                .AddTransient<IBluetoothDevice, BluetoothDevice>()
                .AddSingleton<IBluetoothSensor, BluetoothSensor>()
                .AddSingleton<IBluetoothDeviceScanner, BluetoothDeviceScanner>();

            return services;
        }
    }
}