using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using MyBooks.Client.Services;
using MyBooks.Dto.Dtos;
using ReactiveUI;

namespace MyBooks.Client.ViewModels
{
    public class NewBookViewModel : ReactiveObject
    {
        private readonly IMyBooksApiClient _myBooksApiClient;

        private string _title;
        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        private string _genre;
        public string Genre
        {
            get => _genre;
            set => this.RaiseAndSetIfChanged(ref _genre, value);
        }

        private string _summary;
        public string Summary
        {
            get => _summary;
            set => this.RaiseAndSetIfChanged(ref _summary, value);
        }

        public ReactiveCommand<Unit, Book> AddNewBookCommand { get; set; }

        public NewBookViewModel(IMyBooksApiClient myBooksApiClient)
        {
            _myBooksApiClient = myBooksApiClient;

            AddNewBookCommand = ReactiveCommand.Create(AddBook);
        }

        public Book AddBook()
        {
            var bookToBeInserted = new Book
            {
                Title = Title,
                Genre = Genre,
                Summary = Summary
            };
            var insertedBook = _myBooksApiClient.InsertBookAsync(bookToBeInserted);
            return insertedBook.Result;
        }
    }
}