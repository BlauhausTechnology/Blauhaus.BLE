using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blauhaus.BLE.Abstractions.Device;
using Blauhaus.Common.Abstractions;
using Blauhaus.Responses;

namespace Blauhaus.BLE.Abstractions.DeviceScanner
{
    public interface IBluetoothDeviceScanner : IAsyncPublisher<IReadOnlyList<Guid>>
    {
        Task<IBluetoothDevice> GetDeviceAsync(Guid id);

        Task<Response> StartScanningAsync();
        Task<Response> StopScanningAsync();

    }
}