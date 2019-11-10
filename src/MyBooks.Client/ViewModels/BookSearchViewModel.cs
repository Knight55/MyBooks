using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyBooks.Client.Services;
using MyBooks.Dto.Dtos;
using ReactiveUI;
using Splat;

namespace MyBooks.Client.ViewModels
{
    public class BookSearchViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IMyBooksApiClient _myBooksApiClient;
        private readonly ILogger<BookSearchViewModel> _logger;

        public string UrlPathSegment => "bookSearch";
        public IScreen HostScreen { get; }

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

        public BookSearchViewModel(
            IScreen hostScreen,
            IMyBooksApiClient myBooksApiClient,
            ILogger<BookSearchViewModel> logger)
        {
            HostScreen = hostScreen;
            _myBooksApiClient = myBooksApiClient;
            _logger = logger;

            // TODO: Create book details view model only from the ID of the book!
            GoToBookDetails = ReactiveCommand.CreateFromTask<Book>(async b =>
            {
                var bookDetailsViewModel = Locator.Current.GetService<BookDetailsViewModel>();
                var book = await _myBooksApiClient.GetBookByGoodreadsId(b.GoodreadsId);
                bookDetailsViewModel.Book = book;
                HostScreen.Router.Navigate.Execute(bookDetailsViewModel).Subscribe();
            });

            GoToBookDetails.ThrownExceptions
                .Subscribe(x =>
                    _logger.LogError($"Exception occured when executing GoToBookDetails: {x.Message}", x));

            _results = this
                .WhenAnyValue(x => x.SearchTerm)
                .Throttle(TimeSpan.FromMilliseconds(1000))
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
                books = await _myBooksApiClient.GetBooksAsync().ConfigureAwait(false);
            }
            else
            {
                books = await _myBooksApiClient.SearchBooksAsync(searchTerm).ConfigureAwait(false);
            }
            return books;
        }
    }
}