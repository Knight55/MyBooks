using ReactiveUI;
using Splat;

namespace MyBooks.Client.ViewModels
{
    /// <summary>
    /// View model for MainWindow. It is used for navigation routing.
    /// </summary>
    public class MainViewModel : ReactiveObject, IScreen
    {
        public RoutingState Router { get; }

        public MainViewModel()
        {
            Router = new RoutingState();
        }
    }
}