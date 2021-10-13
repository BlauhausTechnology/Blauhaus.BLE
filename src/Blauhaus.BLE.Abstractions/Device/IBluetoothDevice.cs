using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blauhaus.BLE.Abstractions.Characteristics;
using Blauhaus.Common.Abstractions;

namespace Blauhaus.BLE.Abstractions.Device
{
    public interface IBluetoothDevice : IAsyncPublisher<BluetoothDeviceState>
    {
        Task<IReadOnlyList<Guid>> GetServiceIdsAsync();
        Task<IBluetoothService> GetServiceAsync(Guid serviceId);
        
        Task<IReadOnlyList<BluetoothAdvertisement>> GetAdvertisementsAsync();

    }
}