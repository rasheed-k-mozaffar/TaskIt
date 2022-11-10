using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskIt.Web.Controllers
{
    [Authorize]
    public class TodosController : Controller
    {
        private readonly ITodoRepository todoRepository;
        
        //Using DI to inject the itodo interface so the controller can access its implementing class (todo repo).
        public TodosController(ITodoRepository _todoRepository)
        {
            todoRepository = _todoRepository;
           
        }


        [HttpGet]
        public IActionResult Index()
        {
            //Calling the todos repo to access the display all method then we filter the results so
            //the page displays todos to their respective user and not everything in the db.
            IEnumerable<Todo> allTodos = todoRepository.DisplayAllTodos().Where(t => t.Owner == User.Identity.Name);
            return View(allTodos);
        }

        [HttpGet]
        public IActionResult AddTodo() => View();

        [HttpPost]
        public IActionResult AddTodo(Todo todo)
        {
            //Creating a todo object and using the already populated properties of the tood model
            //passed as an argument to instanitate the new todo object.
            Todo _todo = new Todo()
            {
                Title = todo.Title,
                Owner = User.Identity.Name
            };

            if (_todo.Title == null) return View();

            todoRepository.AddTodo(_todo);
            TempData["success"] = "New Todo Was Added Successfully :)";
            return RedirectToAction("Index");
            
        }

        [HttpPost]
        public IActionResult RemoveTodo(long id)
        {
            todoRepository.RemoveTodo(id);
            TempData["success"] = "Todo Was Removed Successfully :)";
            return RedirectToAction("Index");
        }

        


       
    }
}

