using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Domain.Entities;

namespace Tasks.Core.Tests.Domain
{
    public class ToDoTests
    {
        [Fact]
        public void Complete_ShouldSetToDoDate_WhenCalled()
        {
            //Arrange
            var todo = new ToDo();

            //Act
            todo.Complete();

            //Assert
            Assert.NotNull(todo.Completed);
        }

        [Fact]
        public void Uncomplete_ShouldNullToDoDate_WhenCalled()
        {
            //Arrange
            var todo = new ToDo();
            todo.Complete();

            //Act
            todo.Uncomplete();

            //Assert
            Assert.Null(todo.Completed);
        }

        [Fact]
        public void Complete_ShouldThrowException_WhenAlreadyCompleted()
        {
            //Arrange
            var todo = new ToDo();
            todo.Complete();

            //Act && Assert
            Assert.Throws<InvalidOperationException>(() => todo.Complete());
        }


        [Fact]
        public void Uncomplete_ShouldThrowException_WhenNotYetCompleted()
        {
            //Arrange
            var todo = new ToDo();

            //Act && Assert
            Assert.Throws<InvalidOperationException>(() => todo.Uncomplete());
        }
    }
}
