using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using MyBooks.Client.Services;
using MyBooks.Dto.Dtos;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MyBooks.Client.ViewModels
{
    public class NewBookViewModel : ReactiveObject
    {
        private readonly IMyBookApiService _myBookApiService;

        [Reactive] public string Title { get; set; }
        [Reactive] public string Genre { get; set; }
        [Reactive] public string Summary { get; set; }

        public ReactiveCommand<Unit, Book> AddNewBookCommand { get; set; }

        public NewBookViewModel(IMyBookApiService myBookApiService)
        {
            _myBookApiService = myBookApiService;

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
            var insertedBook = _myBookApiService.InsertBookAsync(bookToBeInserted);
            return insertedBook.Result;
        }
    }
}