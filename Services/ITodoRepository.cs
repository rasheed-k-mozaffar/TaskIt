using System;
namespace TaskIt.Web.Services
{
    public interface ITodoRepository
    {
        //signatures for performing Create , update , and delete methods on todo items.
        void AddTodo(Todo todo);
        void UpdateTodo(Todo todo);
        void RemoveTodo(long id);
        
        
        //searching for a specific todo item using in id of type long.
        Todo FindById(long id);

        //getting all the todo items available for their respective owner(User).
        IEnumerable<Todo> DisplayAllTodos();
        

    }
}

    