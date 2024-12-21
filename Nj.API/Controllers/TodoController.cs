using Microsoft.AspNetCore.Mvc;
using Nj.Infrastructure.Models;

namespace Nj.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> TodoItems = new List<TodoItem>();
        private static int NextId = 1;


        /// <summary>
        /// Create 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateTodoItem([FromBody] TodoItem newTodo)
        {
            newTodo.Id = NextId++;
            TodoItems.Add(newTodo);
            return CreatedAtAction(nameof(GetTodoItemById), new { id = newTodo.Id }, newTodo);
        }
        /// <summary>
        /// Retrieve 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllTodoItems()
        {
            return Ok(TodoItems);
        }

        /// <summary>
        /// Retrieve a single Todo item by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetTodoItemById(int id)
        {
            var todo = TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        /// <summary>
        ///  Retrieve pending (not completed) Todo items
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("pending")]
        public IActionResult GetPendingTodoItems()
        {
            var pendingTodos = TodoItems.Where(t => !t.IsCompleted).ToList();
            return Ok(pendingTodos);
        }
        /// <summary>
        /// Mark a Todo item as completed
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut("{id}/complete")]
        public IActionResult MarkTodoItemAsCompleted(int id)
        {
            var todo = TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsCompleted = true;
            return NoContent();
        }
    }
}

