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
using System.Windows.Shapes;
using MyBooks.Client.ViewModels;
using ReactiveUI;
using Splat;

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
            ViewModel = Locator.Current.GetService<NewBookViewModel>();

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
