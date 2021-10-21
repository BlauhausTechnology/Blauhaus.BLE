using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blauhaus.Analytics.Abstractions.Extensions;
using Blauhaus.Analytics.Abstractions.Service;
using Blauhaus.BLE.Abstractions;
using Blauhaus.BLE.Abstractions.Devices;
using Blauhaus.Common.Utils.Disposables;
using Blauhaus.Errors;
using Blauhaus.Ioc.Abstractions;
using Blauhaus.Responses;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;

namespace Blauhaus.BLE.Devices
{
    public class BluetoothDeviceScanner : BasePublisher, IBluetoothDeviceScanner
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly IServiceLocator _serviceLocator;
        private readonly IAdapter _bleAdapter;
        private bool _isSubscribedToAdapter;
        private bool _isScanning;
        private readonly Dictionary<Guid, IBluetoothDevice> _discoveredDevices = new();

        public BluetoothDeviceScanner(
            IAnalyticsService analyticsService,
            IServiceLocator serviceLocator)
        {
            _analyticsService = analyticsService;
            _serviceLocator = serviceLocator;
            _bleAdapter = CrossBluetoothLE.Current.Adapter;
        }

        public async Task<IDisposable> SubscribeAsync(Func<IReadOnlyList<Guid>, Task> handler, Func<IReadOnlyList<Guid>, bool>? filter = null)
        {
            var token = AddSubscriber(handler, filter);
            if (!_isSubscribedToAdapter)
            {
                _bleAdapter.DeviceDiscovered += HandleDeviceDiscoveredAsync;

                _isSubscribedToAdapter = true;
            }

            await PublishUpdateAsync();

            return token;
        }

        private async void HandleDeviceDiscoveredAsync(object sender, DeviceEventArgs e)
        {
            _discoveredDevices[e.Device.Id] = await _serviceLocator.ResolveAndInitializeAsync<IBluetoothDevice, Guid>(e.Device.Id);

            _analyticsService.Debug($"Bluetooth device discovered: {e.Device.Id}. Total discovered: {_discoveredDevices.Count}");
             
            await PublishUpdateAsync();
        }

        public async Task<Response<IBluetoothDevice>> GetDeviceAsync(Guid id)
        {
            if(!_discoveredDevices.TryGetValue(id, out var device))
            {
                return _analyticsService.TraceErrorResponse<IBluetoothDevice>(this, BluetoothError.DeviceNotFound);
            }

            return Response.Failure<IBluetoothDevice>(Error.Cancelled);
        }

        public async Task<Response> ScanAsync()
        {
            if (!_isScanning)
            {
                _isScanning = true;

                _discoveredDevices.Clear();
                await PublishUpdateAsync();

                try
                {
                    await _bleAdapter.StartScanningForDevicesAsync();
                }
                catch (Exception e)
                {
                    return Response.Failure(e.Message);
                }
                finally
                {
                    _isScanning = false;
                }
            }
            return Response.Success();
        }

        private async Task PublishUpdateAsync()
        {
            var deviceIds = (IReadOnlyList<Guid>)_discoveredDevices.Keys.ToArray();
            await UpdateSubscribersAsync(deviceIds);
        }
    }
}