using Prism;
using Prism.Ioc;
using XFVidyoSample.ViewModels;
using XFVidyoSample.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFVidyo.Core;
using System.Diagnostics;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XFVidyoSample
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/LobbyPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IPermissions>(CrossPermissions.Current);

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LobbyPage, LobbyPageViewModel>();
            containerRegistry.RegisterForNavigation<RoomPage, RoomPageViewModel>();
        }

        protected override void OnStart()
        {
            Debug.WriteLine("OnStart");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Debug.WriteLine("OnSleep");
            var vidyoController = this.Container.Resolve<IVidyoController>();
            vidyoController.OnAppSleep();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            Debug.WriteLine("OnResume");
            var vidyoController = this.Container.Resolve<IVidyoController>();
            vidyoController.OnAppResume();
        }
    }
}
