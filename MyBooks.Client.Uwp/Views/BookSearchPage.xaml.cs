using System.Reactive.Disposables;
using Windows.UI.Xaml;
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
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register(
                nameof(ViewModel),
                typeof(BookSearchViewModel),
                typeof(BookSearchPage),
                new PropertyMetadata(null));

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

        public BookSearchViewModel ViewModel
        {
            get => (BookSearchViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (BookSearchViewModel)value;
        }
    }
}
