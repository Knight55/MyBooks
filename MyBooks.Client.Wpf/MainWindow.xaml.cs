using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyBooks.Client.ViewModels;
using MyBooks.Client.Wpf.Views;
using ReactiveUI;
using Splat;

namespace MyBooks.Client.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<AppViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = Locator.Current.GetService<AppViewModel>(); //new AppViewModel(Locator.Current.GetService<IScreen>());

            this.WhenActivated(disposableRegistration =>
            {
                this.Bind(ViewModel, vm => vm.HostScreen.Router, v => v.viewHost.Router)
                    .DisposeWith(disposableRegistration);
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
    }
}
