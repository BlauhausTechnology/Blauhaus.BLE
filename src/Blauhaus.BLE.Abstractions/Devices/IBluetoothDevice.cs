using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blauhaus.BLE.Abstractions.Services;
using Blauhaus.Common.Abstractions;

namespace Blauhaus.BLE.Abstractions.Devices
{
    public interface IBluetoothDevice : IAsyncPublisher<BluetoothDeviceState>, IAsyncInitializable<Guid> 
    {
        Task<IReadOnlyList<Guid>> GetServiceIdsAsync();
        Task<IBluetoothService> GetServiceAsync(Guid serviceId);
        
        Task<IReadOnlyList<BluetoothAdvertisement>> GetAdvertisementsAsync();

    }
}