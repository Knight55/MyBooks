using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using MyBooks.Client.ViewModels;
using MyBooks.Dto.Dtos;
using ReactiveUI;

namespace MyBooks.Client.Wpf.Views
{
    /// <summary>
    /// Interaction logic for BookSearchView.xaml
    /// </summary>
    public partial class BookSearchView : ReactiveUserControl<BookSearchViewModel>
    {
        public BookSearchView()
        {
            InitializeComponent();

            this.WhenActivated(disposableRegistration =>
            {
                this.OneWayBind(ViewModel, vm => vm.Results, v => v.bookResultsListBox.ItemsSource)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel, vm => vm.SearchTerm, v => v.searchTextBox.Text)
                    .DisposeWith(disposableRegistration);

                IObservable<EventPattern<SelectionChangedEventArgs>> selectionChangedObservable =
                    Observable.FromEventPattern<SelectionChangedEventArgs>(bookResultsListBox, "SelectionChanged");

                selectionChangedObservable
                    .Subscribe(evt =>
                    {
                        var listBox = (ListBox)evt.Sender;
                        var book = (Book)listBox?.SelectedItem;
                        if (book != null)
                        {
                            Debug.WriteLine($"Clicked on book: {book?.Title}");
                            ViewModel.GoToBookDetails.Execute(book).Subscribe();
                        }
                    });
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnDeleteButtonClicked(object sender, RoutedEventArgs args)
        {
            var button = (Button) sender;
            var book = (Book) button?.DataContext;
            if (book != null)
            {
                ViewModel.DeleteBookCommand.Execute(book.Id).Subscribe();
            }
        }
    }
}
