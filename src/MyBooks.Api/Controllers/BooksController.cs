﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBooks.Api.Services;
using MyBooks.Bll.Services;
using MyBooks.Dto.Dtos;

namespace MyBooks.Api.Controllers
{
    /// <summary>
    /// Public API controller for books.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly GoodreadsService _goodreadsService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookService"></param>
        /// <param name="mapper"></param>
        /// <param name="goodreadsService"></param>
        public BooksController(IBookService bookService, IMapper mapper, GoodreadsService goodreadsService)
        {
            _bookService = bookService;
            _mapper = mapper;
            _goodreadsService = goodreadsService;
        }

        /// <summary>
        /// Get a list of all books.
        /// </summary>
        /// <returns>Returns all books.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<Book>>(_bookService.GetBooks()));
        }

        /// <summary>
        /// Get a specific book with the given identifier.
        /// </summary>
        /// <param name="id">Identifier of the book.</param>
        /// <returns>Returns a specific book with the given identifier.</returns>
        /// <response code="200">Returns a specific book with the given identifier.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public IActionResult Get(int id)
        {
            return Ok(_mapper.Map<Book>(_bookService.GetBook(id)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [HttpGet("Search/{searchTerm}")]
        [ProducesResponseType(typeof(List<Book>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Search(string searchTerm)
        { 
            // Test
            var response = await _goodreadsService.SearchBooks(searchTerm);
            if (response != null)
            {
                var books = response.SearchResult.Works
                    .Select(work => new Book(work))
                    .ToList();

                return Ok(books);
            }

            return NotFound();
            //return Ok(_mapper.Map<List<Book>>(_bookService.SearchBooks(searchTerm)));
        }

        /// <summary>
        /// Insert the given book.
        /// </summary>
        /// <param name="book">The book to be inserted.</param>
        /// <returns>Returns the created book.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), (int)HttpStatusCode.BadRequest)]
        public IActionResult Post([FromBody] Book book)
        {
            var created = _bookService.InsertBook(_mapper.Map<Dal.Entities.Book>(book));
            return CreatedAtAction(nameof(Get), new { created.Id }, _mapper.Map<Book>(created));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            _bookService.UpdateBook(id, _mapper.Map<Dal.Entities.Book>(book));
            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookService.DeleteBook(id);
            return NoContent();
        }
    }
}
