using Application.Authors.Commands.CreateAuthor;
using Application.Authors.Commands.DeleteAuthor;
using Application.Authors.Commands.UpdateAuthor;
using Application.Authors.Queries.GetAuthorById;
using Application.Authors.Queries.GetAuthorList;
using Application.Books.Commands.CreateBook;
using Application.Books.Commands.Delete_Book;
using Application.Books.Commands.UpdateBook;
using Application.Books.Queries.GetBookList;
using AutoMapper;
using Bookify.Domain.Model;
using Bookify.Dto;
using Bookify.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.EventSource;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public AuthorController(IMapper mapper, IMediator mediator, ILogger<AuthorController> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation(LogEvents.GetItem, "Get all authors");
            var result = await _mediator.Send(new GetAuthorListQuery());
            var mappedResult = _mapper.Map<List<AuthorGetDto>>(result);
            return Ok(mappedResult);
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation(LogEvents.GetItem,"GET Author id {id}", id);
            var result = await _mediator.Send(new GetAuthorByIdQuery { Id = id });
            if(result == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound,"Author id {id} not found", id);
                return NotFound();
            }
            var mappedResult = _mapper.Map<AuthorGetDto>(result);
            return Ok(mappedResult);
        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AuthorPutPostDto value)
        {
            _logger.LogInformation(LogEvents.InsertItem,"Post Author {name},{description}",value.Name,value.Description);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Post author bad request");
                return BadRequest(ModelState);
            }
            var command = new CreateAuthorCommand
            {
                Name=value.Name,
                Description=value.Description
            };
            var result = await _mediator.Send(command);
            var mappedResult = _mapper.Map<AuthorGetDto>(result);

            return CreatedAtAction(nameof(Get), new { id = mappedResult.Id }, mappedResult);
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AuthorPutPostDto value)
        {
            _logger.LogInformation(LogEvents.UpdateItem,"Post Author id {id}", id);
            var command = new UpdateAuthorCommand
            {
                AuthorId = id,
                Name = value.Name,
                Description = value.Description,
            };
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogWarning(LogEvents.UpdateItemNotFound,"Author id {id} not found", id);
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation(LogEvents.DeleteItem,"Delete Author id {id}", id);
            var command = new DeleteAuthorCommand { AuthorId = id };
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogWarning(LogEvents.DeleteItemNotFound,"Author id {id} not found", id);
                return NotFound();
            };

            return NoContent();
        }
    }
}
