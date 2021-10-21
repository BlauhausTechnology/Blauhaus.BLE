using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Blauhaus.Analytics.Abstractions.Service;
using Blauhaus.BLE.Abstractions.BluetoothSensor;
using Blauhaus.Common.Utils.Disposables;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;

namespace Blauhaus.BLE.BluetoothSensors
{
    public class BluetoothSensor : BasePublisher, IBluetoothSensor
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly IBluetoothLE _ble;
        private BluetoothState _currentState;
        private bool _isSubscribed;

        public BluetoothSensor(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;

            _ble = CrossBluetoothLE.Current;
        }


        public async Task<IDisposable> SubscribeAsync(Func<BluetoothSensorState, Task> handler, Func<BluetoothSensorState, bool>? filter = null)
        {
            var disposable = AddSubscriber(handler, filter);

            if (!_isSubscribed)
            {
                _currentState = _ble.State;
                _ble.StateChanged += HandleBluetoothStateChangedAsync;
                _isSubscribed = true;
            }
            
            _analyticsService.Trace(this, $"Current bluetooth state: {_ble.State}");
            await UpdateSubscribersAsync(_ble.State);

            return disposable;
        }
        
        private async void HandleBluetoothStateChangedAsync(object sender, BluetoothStateChangedArgs e)
        {
            if (e.NewState != _currentState)
            {
                _currentState = e.NewState;

                _analyticsService.Trace(this, $"Bluetooth state changed from {e.OldState} to {e.NewState}");

                var state = e.NewState switch
                {
                    BluetoothState.Off => BluetoothSensorState.Off,
                    BluetoothState.On => BluetoothSensorState.On,
                    BluetoothState.Unknown => BluetoothSensorState.Unknown,
                    BluetoothState.Unavailable => BluetoothSensorState.Unavailable,
                    BluetoothState.Unauthorized => BluetoothSensorState.Unauthorized,
                    BluetoothState.TurningOn => BluetoothSensorState.TurningOn,
                    BluetoothState.TurningOff => BluetoothSensorState.TurningOff,
                    _ => BluetoothSensorState.Unknown
                };

                await UpdateSubscribersAsync(state);
            }
        }
        
    }
}