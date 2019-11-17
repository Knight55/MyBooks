using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyBooks.Client.Services;
using MyBooks.Dto.Dtos;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace MyBooks.Client.ViewModels
{
    public class BookSearchViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IMyBooksApiClient _myBooksApiClient;
        private readonly ILogger<BookSearchViewModel> _logger;

        public string UrlPathSegment => "bookSearch";
        public IScreen HostScreen { get; }

        [Reactive] public string SearchTerm { get; set; }

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

            GoToBookDetails = ReactiveCommand.Create<Book>(b =>
            {
                var bookDetailsViewModel = Locator.Current.GetService<BookDetailsViewModel>();
                bookDetailsViewModel.GoodreadsId = b.GoodreadsId;
                HostScreen.Router.Navigate.Execute(bookDetailsViewModel).Subscribe();
            });

            GoToBookDetails.ThrownExceptions
                .Subscribe(ex =>
                    _logger.LogError($"Exception occurred when executing GoToBookDetails: {ex.Message}", ex));

            _results = this
                .WhenAnyValue(x => x.SearchTerm)
                .Throttle(TimeSpan.FromMilliseconds(1000))
                .Select(term => term?.Trim())
                .DistinctUntilChanged()
                .SelectMany(GetBooks)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToProperty(this, x => x.Results);

            _results.ThrownExceptions
                .Subscribe(ex =>
                    _logger.LogError($"Exception occurred when getting search results: {ex.Message}", ex));

            _isAvailable = this
                .WhenAnyValue(x => x.Results)
                .Select(results => results != null)
                .ToProperty(this, x => x.IsAvailable);
        }

        /// <summary>
        /// Searches books by their title. If no search term is given then returns with every book available.
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