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
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public AuthorController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAuthorListQuery());
            var mappedResult = _mapper.Map<List<AuthorGetDto>>(result);
            return Ok(mappedResult);
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetAuthorByIdQuery { Id = id });
            var mappedResult = _mapper.Map<List<AuthorGetDto>>(result);
            return Ok(mappedResult);
        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthorPutPostDto value)
        {
            if (!ModelState.IsValid)
            {
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
            var command = new UpdateAuthorCommand
            {
                AuthorId = id,
                Name = value.Name,
                Description = value.Description,
            };
            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteAuthorCommand { AuthorId = id };
            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }
    }
}
