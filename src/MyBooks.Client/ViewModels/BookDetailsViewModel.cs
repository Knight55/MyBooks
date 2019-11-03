using System;
using System.Diagnostics;
using System.Reactive;
using MyBooks.Client.Services;
using MyBooks.Dto.Dtos;
using ReactiveUI;

namespace MyBooks.Client.ViewModels
{
    public class BookDetailsViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IMyBooksApiClient _myBooksApiClient;

        public string UrlPathSegment => "bookDetails";
        public IScreen HostScreen { get; }
        
        public ReactiveCommand<Unit, Unit> GoBack { get; }
        public ReactiveCommand<Unit, Unit> OpenGoodreadsUrl { get; }
        public ReactiveCommand<Unit, Unit> UpdateBookCommand { get; }

        public Book Book { get; set; }

        public Uri CoverUrl => new Uri(Book.CoverImageUrl);
        public string Title => Book.Title;

        public string Summary
        {
            get => Book.Summary;
            set => Book.Summary = value;
        }
        public string Genre => Book.Genre;
        //public string Authors => string.Join(", ", Book.Authors.Select(a => a.Name));
        public string Rating => Book.Rating > 0.0 ? $"{Math.Round(Book.Rating, 2)}" : "Not yet rated.";

        public BookDetailsViewModel(IScreen hostScreen, IMyBooksApiClient myBooksApiClient)
        {
            HostScreen = hostScreen;
            _myBooksApiClient = myBooksApiClient;
            GoBack = ReactiveCommand.Create(() => { HostScreen.Router.NavigateBack.Execute(); });
            OpenGoodreadsUrl = ReactiveCommand.Create(() => { Process.Start(Book.GoodreadsUrl); });
            UpdateBookCommand = ReactiveCommand.Create(UpdateBook);
        }

        public void UpdateBook()
        {
            _myBooksApiClient.UpdateBookAsync(Book.Id, Book);
        }
    }
}