using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExceptions;

namespace Repositories.Tests
{

    [TestClass]
    public class TodoRepositoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullToDatabaseThrowsException()
        {
            ITodoRepository repository = new TodoRepository();
            repository.Add(null);
        }

        [TestMethod]
        public void AddingItemWillAddToDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }

        [TestMethod]
        [ExpectedException(typeof(MyExceptions.DuplicateTodoItemException))]
        public void AddingExistingItemWillThrowException()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            repository.Add(todoItem);
        }

        [TestMethod]
        public void GetItemWillReturnNull()
        {
            ITodoRepository repository = new TodoRepository();
            Assert.IsTrue(repository.Get(new Guid()) == null);
        }

        [TestMethod]
        public void GetItemWillReturnItem()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("first");
            repository.Add(item);
            Assert.IsTrue(repository.Get(item.Id).Equals(item));
        }

        [TestMethod]
        public void RemoveItemThatDoesntExists()
        {
            ITodoRepository repository = new TodoRepository();
            Assert.IsTrue(repository.Remove(new Guid()) == false);
        }

        [TestMethod]
        public void RemoveItemReturnsTrue()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("first");
            repository.Add(item);
            Assert.IsTrue(repository.Remove(item.Id) == true);
            Assert.IsTrue(repository.GetAll().Count() == 0);
        }

        [TestMethod]
        public void UpdateAddsNewItem()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("first");
            repository.Update(item);
            Assert.IsTrue(repository.GetAll().Count() == 1);

        }

        [TestMethod]
        public void UpdateChangesExistingItem()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("first");
            repository.Update(item);
            item.IsCompleted = true;
            repository.Update(item);
            Assert.IsTrue(repository.GetAll().Count() == 1);
        }

        [TestMethod]
        public void MarkAsCompletedReturnsTrue()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("first");
            repository.Add(item);
            Assert.IsTrue(repository.Get(item.Id).IsCompleted == false);
            Assert.IsTrue(repository.MarkAsCompleted(item.Id) == true);
            Assert.IsTrue(repository.Get(item.Id).IsCompleted == true);
        }

        [TestMethod]
        public void GetAllItems()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item1 = new TodoItem("1");
            TodoItem item2 = new TodoItem("2");
            TodoItem item3 = new TodoItem("3");
            TodoItem item4 = new TodoItem("4");
            repository.Add(item1);
            repository.Add(item2);
            repository.Add(item3);
            Assert.IsTrue(repository.GetAll().Count() == 3);
            repository.Add(item4);
            Assert.IsTrue(repository.GetAll().Count() == 4);
        }

        [TestMethod]
        public void GetActive()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item1 = new TodoItem("1");
            TodoItem item2 = new TodoItem("2");
            item2.IsCompleted = true;
            TodoItem item3 = new TodoItem("3");
            TodoItem item4 = new TodoItem("4");
            repository.Add(item1);
            repository.Add(item2);
            repository.Add(item3);
            Assert.IsTrue(repository.GetActive().Count() == 2);
            item4.IsCompleted = true;
            repository.Add(item4);
            Assert.IsTrue(repository.GetActive().Count() == 2);
        }

        [TestMethod]
        public void GetCompleted()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item1 = new TodoItem("1");
            TodoItem item2 = new TodoItem("2");
            item2.IsCompleted = true;
            TodoItem item3 = new TodoItem("3");
            TodoItem item4 = new TodoItem("4");
            repository.Add(item1);
            repository.Add(item2);
            repository.Add(item3);
            Assert.IsTrue(repository.GetCompleted().Count() == 1);
            item4.IsCompleted = true;
            repository.Add(item4);
            Assert.IsTrue(repository.GetCompleted().Count() == 2);
        }

        [TestMethod]
        public void GetFiltered()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item1 = new TodoItem("1");
            TodoItem item2 = new TodoItem("2");
            item2.IsCompleted = true;
            TodoItem item3 = new TodoItem("3");
            TodoItem item4 = new TodoItem("4");
            repository.Add(item1);
            repository.Add(item2);
            repository.Add(item3);
            repository.Add(item4);
            Assert.IsTrue(repository.GetFiltered(i => i.IsCompleted == true).Count() == 1);
        }


    }
}