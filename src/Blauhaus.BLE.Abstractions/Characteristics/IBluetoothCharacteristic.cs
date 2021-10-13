using System;
using System.Threading;
using System.Threading.Tasks;
using Blauhaus.Responses;

namespace Blauhaus.BLE.Abstractions.Characteristics
{
    public interface IBluetoothCharacteristic
    {
        Guid Id { get; }
        string Name { get; }

        Task<byte[]> ReadAsync(CancellationToken token = default);
        Task<Response> WriteAsync(byte[] bytes, CancellationToken token = default);
    }
}