using Application.Books.Commands.CreateBook;
using Application.Books.Queries.GetBookById;
using Application.Books.Queries.GetBookList;
using AutoMapper;
using Bookify.Controllers;
using Bookify.Domain.Model;
using Bookify.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework.Internal;
using System.Net;
using System.Text;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        [TestMethod]

        public async Task Get_All_Books_GetAllBooksQueryIsCalled()
        {
            _mockMediator.Setup(m => m.Send(It.IsAny<GetBookListQuery>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new BookController(_mockMediator.Object,_mockMapper.Object);
            await controller.Get();

            _mockMediator.Verify(x=>x.Send(It.IsAny<GetBookListQuery>(),It.IsAny<CancellationToken>()),Times.Once());
        }

        [TestMethod]
        public async Task Get_Book_By_Id_GetBookQueryWithCorrectBookIdIsCalled()
        {
            int bookId = 0;
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            _mockMediator.Setup(m => m.Send(It.IsAny<GetBookByIdQuery>(), It.IsAny<CancellationToken>()))
                .Returns<GetBookByIdQuery, CancellationToken>(async (q, c) =>
                {
                    bookId = q.BookId;
                    return await Task.FromResult(
                        new Book
                        {
                            Id = q.BookId,
                            Title="test",
                            Description="test",
                            ReleaseDate=DateTime.Now,
                            Status=(Status)0,
                            ViewCount=0,
                            Content= bytes
                        });
                });

            var controller = new BookController(_mockMediator.Object,_mockMapper.Object);

            await controller.Get(1);



            Assert.AreEqual(bookId, 1);
        }

        [TestMethod]
        public async Task CallPost_ReturnsBook()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.txt");
            var createBookCommand = new CreateBookCommand
            {
                Title="test",
                Description="Test",
                ReleaseDate=DateTime.Now,
                ImageUrl= "https://play-lh.googleusercontent.com/_tslXR7zUXgzpiZI9t70ywHqWAxwMi8LLSfx8Ab4Mq4NUTHMjFNxVMwTM1G0Q-XNU80",
                Content= file
            };
            var book = new BookPutPostDto
            {
                Title = "test",
                Description = "Test",
                ImageUrl = "https://play-lh.googleusercontent.com/_tslXR7zUXgzpiZI9t70ywHqWAxwMi8LLSfx8Ab4Mq4NUTHMjFNxVMwTM1G0Q-XNU80",
                Content = file
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<CreateBookCommand>(), It.IsAny<CancellationToken>()))
                .Returns<CreateBookCommand, CancellationToken>(async (q, c) =>
                {
                    return await Task.FromResult(
                        new Book
                        {
                            Id = 1,
                            Title = "test",
                            Description = "test",
                            ReleaseDate = DateTime.Now,
                            Status = (Status)0,
                            ViewCount = 0,
                            Content = bytes
                        });
                });
            var controller = new BookController(_mockMediator.Object, _mockMapper.Object);

            var result = await controller.Post(book);

            var okResult = result as OkObjectResult;
            Assert.AreEqual(createBookCommand.Description, ((BookGetDto)okResult.Value).Description);
        }
    }
}