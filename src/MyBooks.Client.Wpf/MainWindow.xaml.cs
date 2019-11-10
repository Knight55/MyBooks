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
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = Locator.Current.GetService<MainViewModel>();

            this.WhenActivated(disposableRegistration =>
            {
                this.Bind(ViewModel, vm => vm.Router, v => v.viewHost.Router)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel, vm => vm.UserManagerCommand, v => v.userManagerButton)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel, vm => vm.IsUserLoggedIn, v => v.userStackPanel.Visibility,
                        conversionHint: BooleanToVisibilityHint.None)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel, vm => vm.IsUserLoggedIn, v => v.userManagerButton.Visibility,
                        conversionHint: BooleanToVisibilityHint.Inverse)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel, vm => vm.UserName, v => v.userNameTextBlock.Text)
                    .DisposeWith(disposableRegistration);

                viewHost.Router.Navigate.Execute(Locator.Current.GetService<BookSearchViewModel>());
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

        // TODO: Move event handlers to MainViewModel
        private void HamburgerMenu_OnItemClick(object sender, ItemClickEventArgs args)
        {
            hamburgerMenu.Content = args.ClickedItem;
            hamburgerMenu.IsPaneOpen = false;
        }

        private void HamburgerMenu_OnOptionsItemClick(object sender, ItemClickEventArgs args)
        {
            throw new System.NotImplementedException();
        }

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
    }
}
