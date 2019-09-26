using System.Reactive.Disposables;
using MyBooks.Client.ViewModels;
using ReactiveUI;

namespace MyBooks.Client.Wpf.Views
{
    /// <summary>
    /// Interaction logic for NewBookWindow.xaml
    /// </summary>
    public partial class NewBookWindow : ReactiveWindow<NewBookViewModel>
    {
        public NewBookWindow()
        {
            InitializeComponent();

            this.WhenActivated(disposableRegistration =>
            {
                this.Bind(ViewModel, vm => vm.Title, v => v.titleTextBox.Text)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel, vm => vm.Genre, v => v.genreTextBox.Text)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel, vm => vm.Summary, v => v.summaryTextBox.Text)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel, vm => vm.AddNewBookCommand, v => v.addNewBookButton)
                    .DisposeWith(disposableRegistration);
            });
        }
    }
}
