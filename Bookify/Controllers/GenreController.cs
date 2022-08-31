
using Application.Genres.Commands.CreateGenre;
using Application.Genres.Commands.DeleteGenre;
using Application.Genres.Queries.GetGenreById;
using Application.Genres.Queries.GetGenreList;
using AutoMapper;
using Bookify.Domain.Model;
using Bookify.Dto;
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
        public GenreController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        // GET: api/<GenreController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetGenreListQuery());
            var mappedResult = _mapper.Map<List<GenreGetDto>>(result);
            return Ok(mappedResult);
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetGenreByIdQuery
            {
                Id = id
            });
            var mappedResult = _mapper.Map<GenreGetDto>(result);
            return Ok(mappedResult);
        }

        // POST api/<GenreController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GenrePutPostDto value)
        {
            if (!ModelState.IsValid)
            {
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
            var command = new DeleteGenreCommand { GenreId = id };
            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }
    }
}
