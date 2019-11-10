using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.Logging;
using MyBooks.Client.ViewModels;
using MyBooks.Dto.Dtos;
using ReactiveUI;
using Splat;

namespace MyBooks.Client.Wpf.Views
{
    /// <summary>
    /// Interaction logic for BookSearchView.xaml
    /// </summary>
    public partial class BookSearchView : ReactiveUserControl<BookSearchViewModel>
    {
        private readonly ILogger<BookSearchView> _logger;

        public BookSearchView()
        {
            InitializeComponent();

            _logger = Locator.Current.GetService<ILogger<BookSearchView>>();

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
                            _logger.LogDebug($"Clicked on book: {book}");
                            ViewModel.GoToBookDetails.Execute(book).Subscribe();
                        }
                    });
            });
        }
    }
}
