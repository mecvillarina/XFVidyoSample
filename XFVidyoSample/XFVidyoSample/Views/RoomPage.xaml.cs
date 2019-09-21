using XFVidyoSample.Common;
using XFVidyoSample.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using XFVidyo.Core;
using XFVidyo.Core.Enums;
using XFVidyo.Core.Providers;

namespace XFVidyoSample.Views
{
    public partial class RoomPage : ContentPage
    {
        private readonly IVidyoController _vidyoController;
        private RoomPageViewModel _viewModel;
        double _pageWidth = 0;
        double _pageHeight = 0;
        bool _hideConfig = false;
        bool _allowReconnect = true;

        public RoomPage(IVidyoController vidyoController)
        {
            InitializeComponent();

            vidyoController.SetNativeView(_videoView);
            vidyoController.OnAppStart();
            _vidyoController = vidyoController;

            INotifyPropertyChanged i = (INotifyPropertyChanged)_vidyoController;
            i.PropertyChanged += new PropertyChangedEventHandler(VidyoControllerPropertyChanged);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel = (RoomPageViewModel)this.BindingContext;
        }
        void VidyoControllerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ConnectorState")
            {
                this.VidyoConnectorState = _vidyoController.ConnectorState;
            }
        }

        void OnConnectButtonClicked(object sender, EventArgs args)
        {
            if (_viewModel.CallAction == VidyoCallAction.VidyoCallActionConnect)
            {
                _viewModel.ToolbarStatus = "Connecting...";

                string token = TokenGeneratorProvider.GenerateToken(VidyoConstants.Key, VidyoConstants.AppId, _viewModel.DisplayName);

                if (!_vidyoController.Connect(VidyoConstants.Host, token, _viewModel.DisplayName, _viewModel.ResourceId))
                {
                    this.VidyoConnectorState = VidyoConnectorState.VidyoConnectorStateConnectionFailure;
                }
                else
                {
                    _viewModel.CallAction = VidyoCallAction.VidyoCallActionDisconnect;
                }
            }
            else
            {
                _viewModel.ToolbarStatus = "Disconnecting...";
                _vidyoController.Disconnect();
            }
        }

        void OnCameraPrivacyButtonClicked(object sender, EventArgs args)
        {
            _vidyoController.SetCameraPrivacy(_viewModel.ToggleCameraPrivacy());
        }

        void OnMicrophonePrivacyButtonClicked(object sender, EventArgs args)
        {
            _vidyoController.SetMicrophonePrivacy(_viewModel.ToggleMicrophonePrivacy());
        }

        void OnCycleCameraButtonClicked(object sender, EventArgs args)
        {
            _vidyoController.CycleCamera();
        }

        VidyoConnectorState VidyoConnectorState
        {
            set
            {
                // UI updates should be made on main thread
                Device.BeginInvokeOnMainThread(() =>
                {
                    // Update the toggle connect button to either start call or end call image
                    _viewModel.CallAction = value == VidyoConnectorState.VidyoConnectorStateConnected ?
                        VidyoCallAction.VidyoCallActionDisconnect : VidyoCallAction.VidyoCallActionConnect;

                    // Set the status text in the toolbar
                    _viewModel.ToolbarStatus = VidyoDefs.StateDescription[value];

                    if (value == VidyoConnectorState.VidyoConnectorStateConnected)
                    {
                        if (!_hideConfig)
                        {
                        }
                    }
                    else
                    {
                        // VidyoConnector is disconnected

                        // If the allow-reconnect flag is set to false and a normal (non-failure) disconnect occurred,
                        // then disable the toggle connect button, in order to prevent reconnection.
                        if (!_allowReconnect && (value == VidyoConnectorState.VidyoConnectorStateDisconnected))
                        {
                            _toggleConnectButton.IsEnabled = false;
                            _viewModel.ToolbarStatus = "Call ended";
                        }

                        if (!_hideConfig)
                        {
                        }
                    }

                });
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (Math.Abs(width - _pageWidth) > 0.001 || Math.Abs(height - _pageHeight) > 0.001)
            {
                _pageWidth = width;
                _pageHeight = height;
                if (_vidyoController != null)
                {
                    _vidyoController.OnOrientationChange();
                }
            }
        }
    }
}
