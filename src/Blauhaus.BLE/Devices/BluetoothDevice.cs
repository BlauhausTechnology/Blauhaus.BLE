using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blauhaus.Analytics.Abstractions.Service;
using Blauhaus.BLE.Abstractions;
using Blauhaus.BLE.Abstractions.Devices;
using Blauhaus.BLE.Abstractions.Services;
using Blauhaus.Common.Utils.Disposables;
using Blauhaus.Errors;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

namespace Blauhaus.BLE.Devices
{
    public class BluetoothDevice : BasePublisher, IBluetoothDevice
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly IAdapter _bleAdapter;
        private IDevice? _device;

        public BluetoothDevice(
            IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
            _bleAdapter = CrossBluetoothLE.Current.Adapter;
        }
         
        public async Task InitializeAsync(Guid deviceId)
        {
            _device = _bleAdapter.DiscoveredDevices.FirstOrDefault(x => x.Id == deviceId);
            if (_device == null)
            {
                throw new ErrorException(BluetoothError.DeviceNotFound);
            }
        }

        public Task<IDisposable> SubscribeAsync(Func<BluetoothDeviceState, Task> handler, Func<BluetoothDeviceState, bool>? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Guid>> GetServiceIdsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IBluetoothService> GetServiceAsync(Guid serviceId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<BluetoothAdvertisement>> GetAdvertisementsAsync()
        {
            throw new NotImplementedException();
        }

    }
}