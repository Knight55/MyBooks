using System.Reactive.Disposables;
using Windows.UI.Xaml.Controls;
using MyBooks.Client.ViewModels;
using ReactiveUI;
using Splat;

namespace MyBooks.Client.Uwp.Views
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class BookSearchPage : Page, IViewFor<BookSearchViewModel>
    {
        public BookSearchPage()
        {
            this.InitializeComponent();

            ViewModel = Locator.Current.GetService<BookSearchViewModel>();

            this.WhenActivated(disposableRegistration =>
            {
                this.OneWayBind(ViewModel, vm => vm.Results, v => v.bookResultsListBox.ItemsSource)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel, vm => vm.SearchTerm, v => v.searchTextBox.Text)
                    .DisposeWith(disposableRegistration);
            });
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (BookSearchViewModel)value;
        }

        public BookSearchViewModel ViewModel
        {
            get => (BookSearchViewModel) DataContext;
            set => DataContext = value;
        }
    }
}
