using System.Windows;

namespace MyBooks.Client.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static AppBootstrapper BootStrapper;

        public App()
        {
            BootStrapper = new AppBootstrapper();
        }
    }
}
