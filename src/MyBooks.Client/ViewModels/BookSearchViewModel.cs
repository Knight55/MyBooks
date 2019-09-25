using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using MyBooks.Client.Services;
using MyBooks.Dto.Dtos;
using ReactiveUI;
using Splat;

namespace MyBooks.Client.ViewModels
{
    public class BookSearchViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "bookSearch";
        public IScreen HostScreen { get; }
        private readonly IMyBookApiService _myBookApiService;

        private string _searchTerm = "";
        public string SearchTerm
        {
            get => _searchTerm;
            set => this.RaiseAndSetIfChanged(ref _searchTerm, value);
        }

        private readonly ObservableAsPropertyHelper<IEnumerable<Book>> _results;
        public IEnumerable<Book> Results => _results.Value;

        private readonly ObservableAsPropertyHelper<bool> _isAvailable;
        public bool IsAvailable => _isAvailable.Value;

        public ReactiveCommand<Book, Unit> GoToBookDetails { get; }
        public ReactiveCommand<int, Unit> DeleteBookCommand { get; }

        public BookSearchViewModel(IScreen hostScreen, IMyBookApiService myBookApiService)
        {
            HostScreen = hostScreen;
            _myBookApiService = myBookApiService;

            GoToBookDetails = ReactiveCommand.Create<Book>(b =>
            {
                var bookDetailsViewModel = Locator.Current.GetService<BookDetailsViewModel>();
                bookDetailsViewModel.Book = b;
                HostScreen.Router.Navigate.Execute(bookDetailsViewModel).Subscribe();
            });
            DeleteBookCommand = ReactiveCommand.Create<int>(DeleteBook);

            _results = this
                .WhenAnyValue(x => x.SearchTerm)
                .Throttle(TimeSpan.FromMilliseconds(700))
                .Select(term => term?.Trim())
                .DistinctUntilChanged()
                .SelectMany(GetBooks)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToProperty(this, x => x.Results);

            _results.ThrownExceptions.Subscribe(ex => { Debug.WriteLine(ex.Message); });

            _isAvailable = this
                .WhenAnyValue(x => x.Results)
                .Select(results => results != null)
                .ToProperty(this, x => x.IsAvailable);
        }

        /// <summary>
        /// Searching books by their title. If no search term is given then
        /// returns with every book available.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Book>> GetBooks(string searchTerm, CancellationToken token)
        {
            List<Book> books;
            if (string.IsNullOrEmpty(searchTerm))
            {
                books = await _myBookApiService.GetBooksAsync().ConfigureAwait(false);
            }
            else
            {
                books = await _myBookApiService.SearchBooksAsync(searchTerm).ConfigureAwait(false);
            }
            return books;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private void DeleteBook(int id)
        {
            _myBookApiService.DeleteBookAsync(id);
        }
    }
}