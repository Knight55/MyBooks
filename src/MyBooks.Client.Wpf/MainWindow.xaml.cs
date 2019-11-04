using System.Reactive.Disposables;
using System.Windows;
using MahApps.Metro.Controls;
using MyBooks.Client.ViewModels;
using MyBooks.Client.Wpf.Views;
using ReactiveUI;
using Splat;

namespace MyBooks.Client.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, IViewFor<MainViewModel>
    {
        //private OidcClient _oidcClient = null;

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = Locator.Current.GetService<MainViewModel>();

            this.WhenActivated(disposableRegistration =>
            {
                this.Bind(ViewModel, vm => vm.Router, v => v.ViewHost.Router)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel, vm => vm.UserManagerCommand, v => v.UserManagerButton)
                    .DisposeWith(disposableRegistration);

                ViewHost.Router.Navigate.Execute(Locator.Current.GetService<BookSearchViewModel>());
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnExit(object sender, RoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnAddNewBook(object sender, RoutedEventArgs args)
        {
            var newBookWindow = new NewBookWindow();
            newBookWindow.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        //private async void OnLogin(object sender, RoutedEventArgs args)
        //{
        //    var options = new OidcClientOptions()
        //    {
        //        Authority = /*"https://demo.identityserver.io/",*/ "http://localhost:5001/",
        //        ClientId = "native.code",
        //        Scope = "openid profile email",
        //        RedirectUri = "https://notused",
        //        ResponseMode = OidcClientOptions.AuthorizeResponseMode.FormPost,
        //        Flow = OidcClientOptions.AuthenticationFlow.AuthorizationCode,
        //        Browser = new WpfEmbeddedBrowser()
        //    };

        //    _oidcClient = new OidcClient(options);

        //    LoginResult result;
        //    try
        //    {
        //        result = await _oidcClient.LoginAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"Unexpected Error: {ex.Message}");
        //        return;
        //    }

        //    if (result.IsError)
        //    {
        //        if (result.Error == "UserCancel")
        //        {
        //            Debug.WriteLine("The sign-in window was closed before authorization was completed.");
        //        }
        //        else
        //        {
        //            Debug.WriteLine($"Error: {result.Error}");
        //        }
        //    }
        //    else
        //    {
        //        var name = result.User.Identity.Name;
        //        Debug.WriteLine($"Hello {name}");
        //    }
        //}

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                "ViewModel",
                typeof(MainViewModel),
                typeof(MainWindow),
                new PropertyMetadata(null));

        /// <inheritdoc/>
        public MainViewModel ViewModel
        {
            get => (MainViewModel) GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MainViewModel) value;
        }

        private void HamburgerMenu_OnItemClick(object sender, ItemClickEventArgs args)
        {
            HamburgerMenu.Content = args.ClickedItem;
            HamburgerMenu.IsPaneOpen = false;
        }

        private void HamburgerMenu_OnOptionsItemClick(object sender, ItemClickEventArgs args)
        {
            throw new System.NotImplementedException();
        }
    }
}
