using System;

namespace TaskIt.Web.Models
{
    public class TodoRepository : ITodoRepository
    {

        
        private readonly ApplicationDbContext _context;

        //Using Dependency injection to inject the db context through the constructor
        public TodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        //This method will be called to add a todo item , it gets its arguments passed to it after recieving the todo from the form.
        public void AddTodo(Todo todo)
        {
             _context.TodoItems.Add(todo);
             _context.SaveChanges();
        }

        //Returns a list of all the existing Todo items in the Db
        public IEnumerable<Todo> DisplayAllTodos()
        {
            IEnumerable<Todo> TodoItems = _context.TodoItems.ToList();
            return TodoItems;
        }

        //This method gets called to select a specific todo item using its id , it could return a null in case the item wasn't found.
        public Todo FindById(long id)
        {
            Todo? todo = _context.TodoItems.Find(id);
            return todo;
        }

        //This method calls the FindById  method to return the item that the user wants to delete.
        public void RemoveTodo(long id)
        {
            Todo todo = FindById(id);
            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
        }

        //This method gets called when a user wants to apply changes to an existing todo.
        public void UpdateTodo(Todo todo)
        {
            _context.TodoItems.Update(todo);
            _context.SaveChanges();
        }
            
        
    }
}

