using System;
using System.Threading.Tasks;
using Blauhaus.BLE.Abstractions.BluetoothSensor;

namespace Blauhaus.BLE.BluetoothSensors
{
    public class BluetoothSensor : IBluetoothSensor
    {
        public Task<IDisposable> SubscribeAsync(Func<BluetoothSensorState, Task> handler, Func<BluetoothSensorState, bool>? filter = null)
        {
            throw new NotImplementedException();
        }
    }
}