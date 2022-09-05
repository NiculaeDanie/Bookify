
using Application.Genres.Commands.CreateGenre;
using Application.Genres.Commands.DeleteGenre;
using Application.Genres.Queries.GetGenreById;
using Application.Genres.Queries.GetGenreList;
using AutoMapper;
using Bookify.Domain.Model;
using Bookify.Dto;
using Bookify.Middleware;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<GenreController> _logger;
        public GenreController(IMapper mapper, IMediator mediator, ILogger<GenreController> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        // GET: api/<GenreController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation(LogEvents.ListItems, "Get all Genres");
            var result = await _mediator.Send(new GetGenreListQuery());
            var mappedResult = _mapper.Map<List<GenreGetDto>>(result);
            return Ok(mappedResult);
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation(LogEvents.GetItem, "GET Genre id {id}", id);
            var result = await _mediator.Send(new GetGenreByIdQuery
            {
                Id = id
            });
            if (result == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Genre id {id} not found", id);
                return NotFound();
            }
            var mappedResult = _mapper.Map<GenreGetDto>(result);
            return Ok(mappedResult);
        }

        // POST api/<GenreController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GenrePutPostDto value)
        {
            _logger.LogInformation(LogEvents.InsertItem, "Post Book {name}", value.Title);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Post Book bad request");
                return BadRequest(ModelState);
            }
            var command = new CreateGenreCommand
            {
                Genre = value.Title
            };
            var result = await _mediator.Send(command);
            var mappedResult = _mapper.Map<GenreGetDto>(result);

            return CreatedAtAction(nameof(Get), new { id = mappedResult.Id }, mappedResult);
        }

        // DELETE api/<GenreController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation(LogEvents.DeleteItem, "Delete Genre id {id}", id);
            var command = new DeleteGenreCommand { GenreId = id };
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogWarning(LogEvents.DeleteItemNotFound, "Genre id {id} not found", id);
                return NotFound();
            };

            return NoContent();
        }
    }
}
