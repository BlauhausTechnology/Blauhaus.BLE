using Blauhaus.Errors;

namespace Blauhaus.BLE.Abstractions
{
    public static class BluetoothError
    {
        public static Error DeviceNotFound = Error.Create("The device can no longer be found");
    }
}