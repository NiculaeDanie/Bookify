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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public BookController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetBookListQuery());
            var mappedResult = _mapper.Map<List<BookGetDto>>(result);
            return Ok(mappedResult);
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetBookByIdQuery
            {
                BookId = id
            });
            var mappedResult = _mapper.Map<BookGetDto>(result);
            return Ok(mappedResult);
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookPutPostDto book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var command = new CreateBookCommand
            {
                Title = book.Title,
                Description = book.Description,
                ReleaseDate = book.RealeaseDate,
                Content = book.Content
            };
            var result = await _mediator.Send(command);
            var mappedResult = _mapper.Map<BookGetDto>(result);

            return CreatedAtAction(nameof(Get), new { id = mappedResult.Id }, mappedResult);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] BookPutPostDto value)
        {
            var command = new UpdateBookCommand
            {
                BookId = id,
                Title = value.Title,
                Content = value.Content,
                Created = value.RealeaseDate
            };
            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteBookCommand { BookId = id };
            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }

        [HttpGet("getbygenre/{id}")]
        public async Task<IActionResult> GetByGenre(int id)
        {
            var result = await _mediator.Send(new GetBookByGenreQuery
            {
                GenreId = id
            });
            var mappedResult = _mapper.Map<BookGetDto>(result);
            return Ok(mappedResult);
        }

        [HttpGet("getcontent/{id}")]
        public async Task<IActionResult> GetContent(int id)
        {
            var result = await _mediator.Send(new GetBookContentQuery
            {
                BookId = id
            });
            return Ok(result);
        }

        [HttpGet("getbyauthor/{id}")]
        public async Task<IActionResult> GetByAuthor(int id)
        {
            var result = await _mediator.Send(new GetAuthorBooksQuery
            {
                AuthorId = id
            });
            var mappedResult = _mapper.Map<BookGetDto>(result);
            return Ok(mappedResult);
        }
    }
}
