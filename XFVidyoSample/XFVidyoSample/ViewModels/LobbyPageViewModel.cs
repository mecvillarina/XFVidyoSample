using System;
using System.Threading.Tasks;
using XFVidyoSample.Common;
using Prism.Commands;
using Prism.Navigation;
using Plugin.Permissions.Abstractions;

namespace XFVidyoSample.ViewModels
{
    public class LobbyPageViewModel : ViewModelBase
    {
        private readonly IPermissions _permissionsUtil;

        public LobbyPageViewModel(INavigationService navigationService, IPermissions permissionsUtil) : base(navigationService)
        {
            _permissionsUtil = permissionsUtil;

            ProceedCommand = new DelegateCommand(async () => await OnProceedCommand(), CanProceedCommandCanExecute).ObservesProperty(() => DisplayName);
        }

        private string _displayName;
        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        private string _roomName;
        public string RoomName
        {
            get => _roomName;
            set => SetProperty(ref _roomName, value);
        }

        public DelegateCommand ProceedCommand { get; private set;  }

        private async Task OnProceedCommand()
        {
            await CheckPermission();

            var navigationParameters = new NavigationParameters();
            navigationParameters.Add(ParameterConstants.DisplayName, DisplayName);
            navigationParameters.Add(ParameterConstants.RoomName, RoomName);
            await NavigationService.NavigateAsync("RoomPage", navigationParameters);
        }

        private bool CanProceedCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(DisplayName);
        }

        private async Task CheckPermission()
        {
            await _permissionsUtil.RequestPermissionsAsync(new Permission[] { Permission.Camera, Permission.Microphone, Permission.Storage });
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var navigationMode = parameters.GetNavigationMode();

            if (navigationMode == NavigationMode.New)
            {
                RoomName = "MondPHRoom";
                DisplayName = "";
            }
        }
    }
}
