using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blauhaus.Common.Abstractions;
using Blauhaus.Responses;

namespace Blauhaus.BLE.Abstractions.Devices
{
    public interface IBluetoothDeviceScanner : IAsyncPublisher<IReadOnlyList<Guid>>
    {
        Task<IBluetoothDevice> GetDeviceAsync(Guid id);

        Task<Response> StartScanningAsync();
        Task<Response> StopScanningAsync();

    }
}