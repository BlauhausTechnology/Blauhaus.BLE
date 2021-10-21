using Blauhaus.Common.Abstractions;

namespace Blauhaus.BLE.Abstractions.BluetoothSensor
{
    public interface IBluetoothSensor : IAsyncPublisher<BluetoothSensorState>
    {
        
    }
}