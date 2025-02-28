using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Application.Data;
using Tasks.Core.Application.Exceptions;
using Tasks.Core.Application.Features.ToDos.CompleteToDo;
using Tasks.Core.Domain.Entities;

namespace Tasks.Core.Tests.Application.Features.ToDos
{
    public class CompleteToDoTests
    {
        private Mock<IToDoRepository> _mockToDoRepository = new();
        private Mock<IUnitOfWork> _mockUnitOfWork = new ();

        [Fact]
        public async void Handle_ShouldThrowException_WhenToDoNotFound()
        {
            //Arrange
            _mockToDoRepository.Setup(x => x.GetToDoAsync(It.IsAny<int>())).ReturnsAsync((ToDo)null);

            var handler = new CompleteToDoCommandHandler(_mockToDoRepository.Object, _mockUnitOfWork.Object);

            // Act && Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(new CompleteToDoCommand { ToDoId = 3 }, default));                      
        }

        [Fact]
        public async void Handle_Finish_WhenToDoFound()
        {
            //Arrange
            _mockToDoRepository.Setup(x => x.GetToDoAsync(3)).ReturnsAsync(new ToDo() { Id = 3});
            _mockUnitOfWork.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var handler = new CompleteToDoCommandHandler(_mockToDoRepository.Object, _mockUnitOfWork.Object);

            // Act
            await handler.Handle(new CompleteToDoCommand { ToDoId = 3 }, default);

            //Assert
            _mockToDoRepository.Verify(x => x.UpdateToDoAsync(It.IsAny<ToDo>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
