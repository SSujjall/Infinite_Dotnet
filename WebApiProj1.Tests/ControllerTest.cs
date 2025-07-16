using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WebApiProj1.Controllers;
using WebApiProj1.Models;
using WebApiProj1.Models.DTOs;
using WebApiProj1.Models.Entities;
using WebApiProj1.Services.Interfaces;

namespace WebApiProj1.Tests
{
    public class ControllerTest
    {
        private IBookService _bookService;
        private BookController _bookController;

        public ControllerTest()
        {
            _bookService = A.Fake<IBookService>();
            _bookController = new BookController(_bookService);
        }

        [Fact]
        public async Task BookController_AddBook_ReturnsOk()
        {
            // ARRANGE
            var inputModel = new AddBooksDTO { BookName = "Test Book", BookPrice = "1000" };
            var expectedResponse = new GenericRes<Books>
            {
                Data = new Books { BookId = 1, BookName = "Test Book", BookPrice = "1000" },

            };

            A.CallTo(() => _bookService.AddBook(inputModel)).Returns(Task.FromResult(expectedResponse));

            // ACT
            var result = await _bookController.CreateBook(inputModel);

            // ASSERT
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualBook = Assert.IsType<GenericRes<Books>>(okResult.Value);
            Assert.Equal(expectedResponse.Data.BookName, actualBook.Data.BookName);
            Assert.Equal(expectedResponse.Data.BookPrice, actualBook.Data.BookPrice);

        }

        [Fact]
        public async Task BookController_GetBookById_ReturnsOk()
        {
            // ARRANGE
            var inputModel = 1;
            var expectedResponse = new GenericRes<Books>
            {
                Data = new Books { BookId = 1, BookName = "Test Book", BookPrice = "1000" },

            };

            A.CallTo(() => _bookService.GetBookById(inputModel)).Returns(Task.FromResult(expectedResponse));

            // ACT
            var result = await _bookController.GetBooks(inputModel);

            // ASSERT
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualBook = Assert.IsType<GenericRes<Books>>(okResult.Value);
            Assert.Equal(expectedResponse.Data.BookId, actualBook.Data.BookId);
            Assert.Equal(expectedResponse.Data.BookName, actualBook.Data.BookName);
            Assert.Equal(expectedResponse.Data.BookPrice, actualBook.Data.BookPrice);

        }
    }
}