using System;
using System.Diagnostics;
using System.Reactive;
using MyBooks.Dto.Dtos;
using ReactiveUI;

namespace MyBooks.Client.ViewModels
{
    public class BookDetailsViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "bookDetails";
        public IScreen HostScreen { get; }

        public ReactiveCommand<Unit, Unit> GoBack { get; }
        public ReactiveCommand<Unit, Unit> OpenGoodreadsUrl { get; }

        public Book Book { get; set; }

        public Uri CoverUrl => new Uri(Book.CoverUrl);
        public string Title => Book.Title;
        public string Summary => Book.Summary ?? "No summary";
        public string Genre => Book.Genre;
        //public string Authors => string.Join(", ", Book.Authors.Select(a => a.Name));
        public string Rating => Book.Rating > 0.0 ? $"{Math.Round(Book.Rating, 2)}" : "Not yet rated.";

        public BookDetailsViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
            GoBack = ReactiveCommand.Create(() => { HostScreen.Router.NavigateBack.Execute(); });
            OpenGoodreadsUrl = ReactiveCommand.Create(() => { Process.Start(Book.GoodreadsUrl); });
        }
    }
}