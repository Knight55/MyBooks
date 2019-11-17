using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyBooks.Client.Services;
using MyBooks.Dto.Dtos;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MyBooks.Client.ViewModels
{
    public class BookDetailsViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly ILogger<BookDetailsViewModel> _logger;
        private readonly IMyBooksApiClient _myBooksApiClient;

        public string UrlPathSegment => "bookDetails";
        public IScreen HostScreen { get; }
        
        public ReactiveCommand<Unit, Unit> GoBack { get; }
        public ReactiveCommand<Unit, Unit> OpenGoodreadsUrl { get; }
        public ReactiveCommand<Unit, Unit> UpdateBookCommand { get; }

        //private readonly ObservableAsPropertyHelper<Book> _book;
        //public Book Book => _book.Value;

        private string _goodreadsId;

        public string GoodreadsId
        {
            get => _goodreadsId;
            set => UpdateBook(value);
        }

        [Reactive] public Uri CoverUrl { get; set; }
        [Reactive] public string Title { get; set; }
        [Reactive] public string Summary { get; set; }
        [Reactive] public string Genre { get; set; }
        [Reactive] public string Rating { get; set; }

        public BookDetailsViewModel(
            IScreen hostScreen,
            ILogger<BookDetailsViewModel> logger,
            IMyBooksApiClient myBooksApiClient)
        {
            HostScreen = hostScreen;
            _logger = logger;
            _myBooksApiClient = myBooksApiClient;

            GoBack = ReactiveCommand.Create(() => { HostScreen.Router.NavigateBack.Execute(); });
            //OpenGoodreadsUrl = ReactiveCommand.Create(() => { Process.Start(Book.GoodreadsUrl); });
            //UpdateBookCommand = ReactiveCommand.Create(UpdateBook);

            //_book = this
            //    .WhenAnyValue(x => x.GoodreadsId)
            //    .SkipWhile(string.IsNullOrWhiteSpace)
            //    .Select(x => Observable.FromAsync(async () =>
            //    {
            //        var book = await _myBooksApiClient.GetBookByGoodreadsId(x);
            //        return book;
            //    }))
            //    .Concat()
            //    .Select(b =>
            //    {
            //        Title = b.Title;
            //        Summary = b.Summary;
            //        return b;
            //    })
            //    .ToProperty(this, x => x.Book);

            //_book.ThrownExceptions
            //    .Subscribe(ex =>
            //        _logger.LogError($"Exception occurred when getting book details: {ex.Message}", ex));
        }

        private async void UpdateBook(string id)
        {
            var book = await _myBooksApiClient.GetBookByGoodreadsId(id);
            CoverUrl = new Uri(book.CoverImageUrl);
            Title = book.Title;
            Summary = book.Summary;
            Genre = book.Genre;
            Rating = book.Rating > 0.0 ? $"{Math.Round(book.Rating, 2)}" : "Not yet rated.";
        }
    }
}