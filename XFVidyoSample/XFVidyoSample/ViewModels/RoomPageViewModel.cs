using XFVidyoSample.Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using XFVidyo.Core;
using XFVidyo.Core.Enums;

namespace XFVidyoSample.ViewModels
{
    public class RoomPageViewModel : ViewModelBase
    {
        public RoomPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            DisplayDiagnostics = false;
        }

        const string _cameraOnImage = "camera_on.png";
        const string _cameraOffImage = "camera_off.png";
        const string _microphoneOnImage = "microphone_on.png";
        const string _microphoneOffImage = "microphone_off.png";
        const string _callStartImage = "call_start.png";
        const string _callEndImage = "call_end.png";
        const string _gearIMage = "gear.png";
        VidyoCallAction _callAction = VidyoCallAction.VidyoCallActionConnect;

        bool _cameraPrivacy = false;
        bool _microphonePrivacy = false;

        public bool DisplayDiagnostics { get; set; }

        private string _host = "prod.vidyo.io";
        public string Host
        {
            get => _host;
            set => SetProperty(ref _host, value);
        }

        private string _displayName;
        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        private string _resourceId;
        public string ResourceId
        {
            get => _resourceId;
            set => SetProperty(ref _resourceId, value);
        }

        private string _toolbarStatus = "Ready to Connect";
        public string ToolbarStatus
        {
            get => _toolbarStatus;
            set => SetProperty(ref _toolbarStatus, value);
        }

        private string _clientVersion = "v 0.0.00.x";
        public string ClientVersion
        {
            get => _clientVersion;
            set => SetProperty(ref _clientVersion, value);
        }

        string _callImage = _callStartImage;
        public string CallImage
        {
            get => _callImage;
            set => SetProperty(ref _callImage, value);
        }

        string _cameraPrivacyImage = _cameraOnImage;
        public string CameraPrivacyImage
        {
            get => _cameraPrivacyImage;
            set => SetProperty(ref _cameraPrivacyImage, value);
        }

        string _microphonePrivacyImage = _microphoneOnImage;
        public string MicrophonePrivacyImage
        {
            get => _microphonePrivacyImage;
            set => SetProperty(ref _microphonePrivacyImage, value);
        }
        public VidyoCallAction CallAction
        {
            get { return _callAction; }
            set
            {
                _callAction = value;
                CallImage = _callAction == VidyoCallAction.VidyoCallActionConnect ? _callStartImage : _callEndImage;
            }
        }

        public bool ToggleCameraPrivacy()
        {
            _cameraPrivacy = !_cameraPrivacy;
            CameraPrivacyImage = _cameraPrivacy ? _cameraOffImage : _cameraOnImage;
            return _cameraPrivacy;
        }

        public bool ToggleMicrophonePrivacy()
        {
            _microphonePrivacy = !_microphonePrivacy;
            MicrophonePrivacyImage = _microphonePrivacy ? _microphoneOffImage : _microphoneOnImage;
            return _microphonePrivacy;
        }

        public VidyoCallAction ToggleCallAction()
        {
            CallAction = _callAction == VidyoCallAction.VidyoCallActionConnect ?
                          VidyoCallAction.VidyoCallActionDisconnect : VidyoCallAction.VidyoCallActionConnect;
            return _callAction;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            ResourceId = parameters.GetValue<string>(ParameterConstants.RoomName);
            DisplayName = parameters.GetValue<string>(ParameterConstants.DisplayName);
        }

    }
}
