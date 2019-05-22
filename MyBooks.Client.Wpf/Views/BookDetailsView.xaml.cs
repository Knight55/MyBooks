using System.Reactive.Disposables;
using System.Windows.Media.Imaging;
using MyBooks.Client.ViewModels;
using ReactiveUI;

namespace MyBooks.Client.Wpf.Views
{
    /// <summary>
    /// Interaction logic for BookDetailsView.xaml
    /// </summary>
    public partial class BookDetailsView : ReactiveUserControl<BookDetailsViewModel>
    {
        public BookDetailsView()
        {
            InitializeComponent();

            this.WhenActivated(disposableRegistration =>
            {
                // Properties
                this.OneWayBind(ViewModel, vm => vm.Title, v => v.titleRun.Text)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel, vm => vm.Genre, v => v.genreRun.Text)
                    .DisposeWith(disposableRegistration);

                //this.OneWayBind(ViewModel, vm => vm.Authors, v => v.authorsRun.Text)
                //    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel, vm => vm.Rating, v => v.ratingRun.Text)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel, vm => vm.Summary, v => v.summaryTextBox.Text)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel, vm => vm.CoverUrl, v => v.coverImage.Source,
                        url => url == null ? null : new BitmapImage(url))
                    .DisposeWith(disposableRegistration);

                // Commands
                this.BindCommand(ViewModel, vm => vm.GoBack, v => v.backButton)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel, vm => vm.OpenGoodreadsUrl, v => v.goodreadsLink)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel, vm => vm.UpdateBookCommand, v => v.updateButton)
                    .DisposeWith(disposableRegistration);
            });
        }
    }
}
