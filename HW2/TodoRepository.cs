using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zad2;
using MyExceptions;

namespace Repositories
{
    /// <summary >
    /// Class that encapsulates all the logic for accessing TodoTtems .
    /// </ summary >
    public class TodoRepository : ITodoRepository
    {
        /// <summary >
        /// Repository does not fetch todoItems from the actual database ,
        /// it uses in memory storage for this excersise .
        /// </ summary >
        public readonly IGenericList<TodoItem> _inMemoryTodoDatabase;

        public TodoRepository(IGenericList<TodoItem> initialDbState = null)
        {
            if (initialDbState != null)
            {
                _inMemoryTodoDatabase = initialDbState;
            }
            else
            {
                _inMemoryTodoDatabase = new GenericList<TodoItem>();
            }
            // Shorter way to write this in C# using ?? operator :
            // _inMemoryTodoDatabase = initialDbState ?? new List < TodoItem >() ;
            // x ?? y -> if x is not null , expression returns x. Else y.
        }


        // implement ITodoRepository


        /// <summary >
        /// Gets TodoItem for a given id
        /// </ summary >
        /// <returns > TodoItem if found , null otherwise </ returns >
        public TodoItem Get(Guid todoId)
        {
           if(_inMemoryTodoDatabase.Where(item => item.Id.Equals(todoId)).Count() >= 1)
            {
                return _inMemoryTodoDatabase.Where(item => item.Id.Equals(todoId)).First();
            }
            return null;

        }

        /// <summary >
        /// Adds new TodoItem object in database .
        /// If object with the same id already exists ,
        /// method should throw DuplicateTodoItemException with the message " duplicate id: {id }".
        /// </ summary >
        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                throw new ArgumentNullException();
            }

            if (Get(todoItem.Id) != null)
            {
                throw new DuplicateTodoItemException(todoItem.Id);
            }

            _inMemoryTodoDatabase.Add(todoItem);
        }

        /// <summary >
        /// Tries to remove a TodoItem with given id from the database .
        /// </ summary >
        /// <returns > True if success , false otherwise </ returns >
        public bool Remove(Guid todoId)
        {
            if (Get(todoId) != null)
            {
                _inMemoryTodoDatabase.Remove(this.Get(todoId));
                return true;
            }
            return false;
        }

        /// <summary >
        /// Updates given TodoItem in database .
        /// If TodoItem does not exist , method will add one .
        /// </ summary >
        public void Update(TodoItem todoItem)
        {
            TodoItem tmp;
            tmp = this.Get(todoItem.Id);
            if (tmp == null)
            {
                _inMemoryTodoDatabase.Add(todoItem);
            }
            else
            {
                this.Remove(todoItem.Id);
                _inMemoryTodoDatabase.Add(todoItem);
            }
        }

        /// <summary >
        /// Tries to mark a TodoItem as completed in database .
        /// </ summary >
        /// <returns > True if success , false otherwise </ returns >
        public bool MarkAsCompleted(Guid todoId)
        {
            TodoItem pom = this.Get(todoId);
            if (pom != null)
            {
                pom.IsCompleted = true;
                this.Update(pom);
                return true;
            }
            return false;
        }

        /// <summary >
        /// Gets all TodoItem objects in database , sorted by date created( descending )
        /// </ summary >
        public List<TodoItem> GetAll()
        {
            return _inMemoryTodoDatabase.OrderByDescending(item => item.DateCreated).ToList();
        }

        /// <summary >
        /// Gets all incomplete TodoItem objects in database
        /// </ summary >
        public List<TodoItem> GetActive()
        {
            return _inMemoryTodoDatabase.Where(item => item.IsCompleted == false).ToList();
        }

        /// <summary >
        /// Gets all completed TodoItem objects in database
        /// </ summary >
        public List<TodoItem> GetCompleted()
        {
            return _inMemoryTodoDatabase.Where(item => item.IsCompleted == true).ToList();

        }

        /// <summary >
        /// Gets all TodoItem objects in database that apply to the filter
        /// </ summary >
        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            return _inMemoryTodoDatabase.Where(filterFunction).ToList();

        }
    }
}
