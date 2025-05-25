using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using KasiCornerKota_Domain.Exceptions;
using KasiCornerKota_Domain.Entities;
using FluentAssertions;

namespace KasiCornerKota_API.Middleware.Tests
{
    public class ErrorHandlingMiddleTests
    {     
        private readonly Mock<ILogger<ErrorHandlingMiddle>> _loggerMock;
        public ErrorHandlingMiddleTests()
        {
            _loggerMock = new Mock<ILogger<ErrorHandlingMiddle>>();
        }
        [Fact()]
        public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextDelegate()
        {
            var context = new DefaultHttpContext();
            var nextDelegate = new Mock<RequestDelegate>();
            var middleware = new ErrorHandlingMiddle(_loggerMock.Object);

            await middleware.InvokeAsync(context, nextDelegate.Object);

            nextDelegate.Verify(next => next.Invoke(It.IsAny<HttpContext>()), Times.Once);
        }
        [Fact()]
        public async Task InvokeAsync_WhenNoExceptionThrown_ShouldSetStatusCode404()
        {
            var context = new DefaultHttpContext();
            var nextDelegate = new Mock<RequestDelegate>();
            var middleware = new ErrorHandlingMiddle(_loggerMock.Object);
            var exception = new NotFoundException(nameof(Restaurant),"1");

            await middleware.InvokeAsync(context, _=> throw exception);

            context.Response.StatusCode.Should().Be(404);
        }
        [Fact()]
        public async Task InvokeAsync_WhenForbidExceptionThrown_ShouldSetStatusCode403()
        {
            var context = new DefaultHttpContext();
            var middleware = new ErrorHandlingMiddle(_loggerMock.Object);
            var forbid = new ForbidException("You are not authorized to access this resource.");

            await middleware.InvokeAsync(context, _ => throw forbid);

            context.Response.StatusCode.Should().Be(403);
        }
        [Fact()]
        public async Task InvokeAsync_WhenExceptionThrown_ShouldSetStatusCode500()
        {
            var context = new DefaultHttpContext();
            var middleware = new ErrorHandlingMiddle(_loggerMock.Object);
            var forbid = new Exception("Something went wrong.");

            await middleware.InvokeAsync(context, _ => throw forbid);

            context.Response.StatusCode.Should().Be(500);
        }
    }
}