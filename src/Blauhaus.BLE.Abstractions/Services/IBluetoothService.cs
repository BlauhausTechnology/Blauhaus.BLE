using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blauhaus.Common.Abstractions;
using Blauhaus.Responses;

namespace Blauhaus.BLE.Abstractions.Services
{
    public interface IBluetoothService : IAsyncPublisher<IReadOnlyList<Guid>>
    {
        Guid Id { get; }
        string Name { get; }

        Task<Response<IBluetoothCharacteristic>> GetCharacteristicAsync(Guid characteristicId);
    }
}