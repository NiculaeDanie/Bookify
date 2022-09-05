using Application.Authors.Commands.CreateAuthor;
using Application.Authors.Commands.DeleteAuthor;
using Application.Authors.Queries.SearchAuthor;
using Application.Books.Queries.Search;
using Application.Users.Commands.AddBookToFavorites;
using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetUserFavorites;
using Application.Users.Queries.GetUserHistory;
using Application.Users.Queries.VerifyUser;
using AutoMapper;
using Bookify.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public UserController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        // GET api/<UserController>/5
        [HttpGet("{email}/{password}")]
        public async Task<IActionResult> Get(string email, string password)
        {

            var result = await _mediator.Send(new VerifyUserQuery
            {
                email = email,
                password = password
            });
            if (result == null)
            {
                return NotFound();
            }
            var mappedResult = _mapper.Map<UserGetDto>(result);
            return Ok(mappedResult);

        }
        [HttpGet("History/{userid}")]
        public async Task<IActionResult> GetHistory(int userid)
        {

            var result = await _mediator.Send(new GetUserHistoryQuery
            {
                UserId = userid
            });
            if (result == null)
            {
                return NotFound();
            }
            var mappedResult = _mapper.Map<List<BookGetDto>>(result);
            return Ok(mappedResult);

        }
        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserPutPostDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var command = new CreateUserCommand
            {
                Name = value.Name,
                Email = value.Email,
                Password = value.Password
            };
            var result = await _mediator.Send(command);
            if(result == null)
            {
                return BadRequest();
            }
            var mappedResult = _mapper.Map<UserGetDto>(result);

            return CreatedAtAction(nameof(Post), new { id = mappedResult.Id }, mappedResult);
        }
        [HttpPost("{bookid}/{id}")]
        public async Task<IActionResult> PostBook(int bookid, int id)
        {
            var result = await _mediator.Send(new AddBookToFavoritesCommand
            {
                UserId = id,
                BookId = bookid
            });
            if (result == null)
                return NoContent();
            return Ok();
        }
        [HttpGet("Favorites/{userid}")]
        public async Task<IActionResult> GetFavorites(int userid)
        {

            var result = await _mediator.Send(new GetUserFavoritesQuery
            {
                UserId = userid
            });
            if (result == null)
            {
                return NotFound();
            }
            var mappedResult = _mapper.Map<List<BookGetDto>>(result);
            return Ok(mappedResult);
        }
        [HttpDelete("{bookid}/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteAuthorCommand { AuthorId = id };
            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{searchstring}")]
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
            foreach(var item in result)
            {
                mappedResult.Add(new AuthorBookGetDto { Id = item.Id, Title=item.Title, Type="Book" });
            }
            foreach (var item in result2)
            {
                mappedResult.Add(new AuthorBookGetDto { Id = item.Id, Title = item.Name, Type = "Author" });
            }
            return Ok(mappedResult);
        }
    }
}
