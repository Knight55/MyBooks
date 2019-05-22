using System.Reflection;
using MyBooks.Client.Services;
using MyBooks.Client.ViewModels;
using ReactiveUI;
using Refit;
using Splat;

namespace MyBooks.Client
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public RoutingState Router { get; }

        public AppBootstrapper()
        {
            Router = new RoutingState();

            // Services
            Locator.CurrentMutable.Register(() =>
                RestService.For<IMyBookApiService>("http://localhost:5000"));

            // ViewModels
            Locator.CurrentMutable.RegisterLazySingleton(() => this, typeof(IScreen));
            Locator.CurrentMutable.RegisterLazySingleton(() => new AppViewModel(
                Locator.Current.GetService<IScreen>()));
            Locator.CurrentMutable.Register(() => new BookSearchViewModel(
                Locator.Current.GetService<IScreen>(),
                Locator.Current.GetService<IMyBookApiService>()
            ), typeof(BookSearchViewModel));
            Locator.CurrentMutable.Register(() => new BookDetailsViewModel(
                Locator.Current.GetService<IScreen>()
            ), typeof(BookDetailsViewModel));

            // Views
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetEntryAssembly());
        }
    }
}