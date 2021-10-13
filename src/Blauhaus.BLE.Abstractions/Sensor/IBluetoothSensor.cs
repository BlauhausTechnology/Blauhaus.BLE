using Blauhaus.Common.Abstractions;

namespace Blauhaus.BLE.Abstractions.Sensor
{
    public interface IBluetoothSensor : IAsyncPublisher<BluetoothSensorState>
    {
        
    }
}