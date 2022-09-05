using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Books.Queries.GetBookList;
using Bookify.Dto;
using Application.Books.Queries.GetBookById;
using Application.Books.Commands.CreateBook;
using Application.Books.Commands.UpdateBook;
using Application.Books.Commands.Delete_Book;
using Application.Books.Queries.GetBookByGenre;
using Application.Users.Queries.GetBookContent;
using Application.Authors.Queries.GetAuthorBooks;
using Application.Authors.Commands.AddBookToAuthor;
using Application.Books.Commands.AddGenreToBook;
using Application.Genres.Queries.GetGenreById;
using Application.Authors.Queries.GetAuthorById;
using Bookify.Domain.Model;
using Application.Books.Queries.GetBookByGenreFiltered;
using Application.Books.Queries.Search;
using Bookify.Middleware;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<BookController> _logger;
        public BookController(IMediator mediator, IMapper mapper, ILogger<BookController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation(LogEvents.ListItems, "Get all Books");
            var result = await _mediator.Send(new GetBookListQuery());
            var mappedResult = _mapper.Map<List<BookGetDto>>(result);
            return Ok(mappedResult);
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation(LogEvents.GetItem, "GET Book id {id}", id);
            var result = await _mediator.Send(new GetBookByIdQuery
            {
                BookId = id
            });
            if (result == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Book id {id} not found", id);
                return NotFound();
            }
            var mappedResult = _mapper.Map<BookGetDto>(result);
            return Ok(mappedResult);
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] BookPutPostDto book)
        {
            _logger.LogInformation(LogEvents.InsertItem, "Post Book {name},{description}", book.Title, book.Description);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Post Book bad request");
                return BadRequest(ModelState);
            }
            var command = new CreateBookCommand
            {
                Title = book.Title,
                Description = book.Description,
                ReleaseDate = book.RealeaseDate,
                Content = book.Content,
                ImageUrl = book.ImageUrl
            };
            var result = await _mediator.Send(command);
            var mappedResult = _mapper.Map<BookGetDto>(result);

            return CreatedAtAction(nameof(Get), new { id = mappedResult.Id }, mappedResult);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] BookPutPostDto value)
        {
            _logger.LogInformation(LogEvents.UpdateItem, "Post Book id {id}", id);
            var command = new UpdateBookCommand
            {
                BookId = id,
                Title = value.Title,
                Content = value.Content,
                Created = value.RealeaseDate
            };
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogWarning(LogEvents.UpdateItemNotFound, "Book id {id} not found", id);
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation(LogEvents.DeleteItem, "Delete Book id {id}", id);
            var command = new DeleteBookCommand { BookId = id };
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogWarning(LogEvents.DeleteItemNotFound, "Book id {id} not found", id);
                return NotFound();
            };

            return NoContent();
        }

        [HttpGet("Genre/{id}")]
        public async Task<IActionResult> GetByGenre(int id)
        {
            _logger.LogInformation(LogEvents.ListItems, "Get all Books with Genre id {id}",id);
            var result = await _mediator.Send(new GetBookByGenreQuery
            {
                GenreId = id
            });
            if (result == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Genre id {id} not found", id);
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<FullBookDto>>(result);
            
            return Ok(mappedResult);
        }

        [HttpGet("Content/{userid}/{id}")]
        public async Task<IActionResult> GetContent(int id, int userid)
        {
            _logger.LogInformation(LogEvents.GetItem, "Get book content with id {id}", id);
            var result = await _mediator.Send(new GetBookContentQuery
            {
                UserId = userid,
                BookId = id
            });
            if (result == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Book id {id} not found", id);
                return NotFound();
            }
            return File(result, "application/pdf", "yourFileName.pdf");
        }

        [HttpGet("Author/{id}")]
        public async Task<IActionResult> GetByAuthor(int id)
        {
            _logger.LogInformation(LogEvents.ListItems, "Get all Books with Author id {id}", id);
            var result = await _mediator.Send(new GetAuthorBooksQuery
            {
               AuthorId = id
            });

            if (result == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Author id {id} not found", id);
                return NotFound();
            };
            var mappedResult = _mapper.Map<List<FullBookDto>>(result);
            return Ok(mappedResult);
        }

        [HttpPut("Author/{id}/{bookid}")]
        public async Task<IActionResult> PutAsyncAuthorToBook(int id, int bookid)
        {
            var command = new AddBookToAuthorCommand
            {
                AuthorId=id,
                BookId=bookid
            };
            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }
        [HttpPut("Genre/{id}/{bookid}")]
        public async Task<IActionResult> PutAsyncGenreToBook(int id, int bookid)
        {
            var command = new AddGenreToBookCommand
            {
                GenreId = id,
                BookId = bookid
            };
            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }
        [HttpGet("Genre/{id}/{userid}")]
        public async Task<IActionResult> GetByGenreFiltered(int id, int userid)
        {

            var result = await _mediator.Send(new GetBookByGenreFilteredQuery
            {
                GenreId = id,
                UserId = userid
            });
            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<List<FullBookDto>>(result);
            foreach(var item in result)
            {
                if (item.UserFavorites.Any(x => x.UserId == userid))
                {
                    mappedResult.Where(x => x.Id == item.Id).Single().IsFavorited = true;
                }
            }
            return Ok(mappedResult);
        }
        
    }
}
