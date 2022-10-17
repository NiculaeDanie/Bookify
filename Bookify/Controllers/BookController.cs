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
using Application.Books.Queries.GetBookImage;
using Application.Users.Queries.GetUserHistory;
using Application.Authors.Queries.SearchAuthor;
using Application.Users.Commands.AddBookToFavorites;
using Application.Users.Commands.DeleteBookFromFavorit;
using Application.Users.Queries.GetUserFavorites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using Microsoft.AspNetCore.Http;

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
        private readonly UserManager<User> _userManager;
        public BookController(IMediator mediator, IMapper mapper, ILogger<BookController> logger, UserManager<User> userManager)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
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
        [HttpGet("{id}/{email}")]
        public async Task<IActionResult> Get(int id,string email)
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
            var mappedResult = _mapper.Map<FullBookDto>(result);
            var user = await _userManager.FindByEmailAsync(email);
            if(result.UserFavorites != null)
            {
                if(result.UserFavorites.Any(x => x.UserId == user.Id))
                {
                    mappedResult.IsFavorited = true;
                }
            }

            return Ok(mappedResult);
        }

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
            var mappedResult = _mapper.Map<FullBookDto>(result);

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
                Image = book.Image
            };
            var result = await _mediator.Send(command);
            var mappedResult = _mapper.Map<BookGetDto>(result);

            return CreatedAtAction(nameof(Get), new { id = mappedResult.Id }, mappedResult);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromForm] BookPostDto value)
        {
            _logger.LogInformation(LogEvents.UpdateItem, "Post Book id {id}", id);
            var command = new UpdateBookCommand
            {
                BookId = id,
                Title = value.Title,
                Content = value.Content,
                Description = value.Description,
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

        [HttpGet("Content/{email}/{id}")]
        public async Task<IActionResult> GetContent(int id, string email)
        {
            _logger.LogInformation(LogEvents.GetItem, "Get book content with id {id}", id);
            var result = await _mediator.Send(new GetBookContentQuery
            {
                UserId = email,
                BookId = id
            });
            if (result == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Book id {id} not found", id);
                return NotFound();
            }
            return File(result.Content, "application/pdf", result.Name); ;
        }
        [HttpGet("Image/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            _logger.LogInformation(LogEvents.GetItem, "Get book image with id {id}", id);
            var result = await _mediator.Send(new GetBookImageQuery
            {
                BookId = id
            });
            if (result == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Book id {id} not found", id);
                return NotFound();
            }
            return File(result.Content, result.ContentType, result.Name); ;
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
        [HttpGet("Genre/{id}/{email}")]
        public async Task<IActionResult> GetByGenreFiltered(int id, string email)
        {

            var result = await _mediator.Send(new GetBookByGenreFilteredQuery
            {
                GenreId = id,
                UserId = email
            });
            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<List<FullBookDto>>(result);
            foreach(var item in result)
            {
                if (item.UserFavorites.Any(x => x.User.Email == email))
                {
                    mappedResult.Where(x => x.Id == item.Id).Single().IsFavorited = true;
                }
            }
            return Ok(mappedResult);
        }
        [HttpGet("History/{email}")]
        public async Task<IActionResult> GetHistory(string email)
        {

            var result = await _mediator.Send(new GetUserHistoryQuery
            {
                UserId = email
            });
            if (result == null)
            {
                return NotFound();
            }
            var mappedResult = _mapper.Map<List<FullBookDto>>(result);
            return Ok(mappedResult);

        }
        [HttpPost("{bookid}/{email}")]
        public async Task<IActionResult> PostBook(int bookid, string email)
        {
            var result = await _mediator.Send(new AddBookToFavoritesCommand
            {
                UserId = email,
                BookId = bookid
            });
            if (result == null)
                return NoContent();
            var mappedResult = _mapper.Map<FullBookDto>(result);
            return Ok(mappedResult);
        }
        [HttpGet("Favorites/{email}")]
        public async Task<IActionResult> GetFavorites(string email)
        {

            var result = await _mediator.Send(new GetUserFavoritesQuery
            {
                UserId = email
            });
            if (result == null)
            {
                return NotFound();
            }
            var mappedResult = _mapper.Map<List<FullBookDto>>(result);
            return Ok(mappedResult);
        }
        [HttpDelete("{bookid}/{email}")]
        public async Task<IActionResult> Delete(string email, int bookid)
        {
            var command = new DeleteBookFromFavoritesCommand { UserId = email, BookId = bookid };
            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();
            var mappedResult = _mapper.Map<BookGetDto>(result);
            return Ok(mappedResult);
        }

        [HttpGet("Search/{searchstring}")]
        public async Task<IActionResult> Search(string searchstring)
        {
            var result = await _mediator.Send(new SearchQuery
            {
                SString = searchstring
            });
            var result2 = await _mediator.Send(new SearchAuthorQuery
            {
                Search = searchstring
            });
            if (result == null && result2 == null)
                return NotFound();
            var mappedResult = new List<AuthorBookGetDto>();
            foreach (var item in result)
            {
                mappedResult.Add(new AuthorBookGetDto { Id = item.Id, Title = item.Title, Type = "Book" });
            }
            foreach (var item in result2)
            {
                mappedResult.Add(new AuthorBookGetDto { Id = item.Id, Title = item.Name, Type = "Author" });
            }
            return Ok(mappedResult);
        }

    }
}
