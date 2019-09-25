using ReactiveUI;
using Splat;

namespace MyBooks.Client.ViewModels
{
    public class AppViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "appView";
        public IScreen HostScreen { get; }

        public AppViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
            HostScreen.Router.Navigate.Execute(Locator.Current.GetService<BookSearchViewModel>());
        }
    }
}