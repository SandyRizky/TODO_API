using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO_API.DataAccess;
using TODO_API.Models;
using TODO_API.Models.Request;
using TODO_API.Models.Response;

namespace TODO_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService iTodoService;
        public TodoController(ITodoService service)
        {
            iTodoService = service;
        }

        [Route("GetAllTodo")]
        [HttpPost]
        public IActionResult GetAllTodo()
        {
            AllTodoResponse response = new AllTodoResponse();
            List<TodoModel> listTodos = iTodoService.getAllTodos();

            response.all_todos = listTodos;

            if (listTodos.Count == 0)
            {
                response.response = "OK";
                response.messages = "No data";
                return Ok(response);
            }

            response.response = "OK";
            response.messages = "Success";
            return Ok(response);
        }

        [Route("GetTodoById")]
        [HttpPost]
        public IActionResult GetTodoById([FromForm] ByIdRequest param)
        {
            GetTodoResponse response = new GetTodoResponse();
            TodoModel todo = iTodoService.getTodoById(param);

            if (todo is null)
            {
                response.response = "Not Found";
                response.messages = "Data Not Found";
                return NotFound(response);
            }

            response.todo = todo;

            response.response = "OK";
            response.messages = "Success";
            return Ok(response);
        }

        [Route("GetIncomingTodo")]
        [HttpPost]
        public IActionResult GetIncomingTodo()
        {
            IncomingTodoResponse response = new IncomingTodoResponse();
            List<TodoModel> todosToday = iTodoService.getIncomingTodoToday();
            List<TodoModel> todosTomorrow = iTodoService.getIncomingTodoTomorrow();
            List<TodoModel> todosThisWeek = iTodoService.getIncomingTodoThisWeek();

            response.today_todo = todosToday;
            response.tomorrow_todo = todosTomorrow;
            response.this_week_todo = todosThisWeek;

            if (todosToday.Count == 0 && todosTomorrow.Count == 0 && todosThisWeek.Count == 0)
            {
                response.response = "OK";
                response.messages = "No data";
                return Ok(response);
            }

            response.response = "OK";
            response.messages = "Success";
            return Ok(response);
        }

        [Route("CreateTodo")]
        [HttpPost]
        public IActionResult CreateTodo([FromForm] CreateTodoRequest param)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GetTodoResponse response = new GetTodoResponse();
            TodoModel todo = iTodoService.CreateTodo(param);

            if (todo.TodoId == null)
            {
                response.response = "Bad Request";
                response.messages = "Failed Insert Data";
                return BadRequest(response);
            }

            response.todo = todo;

            response.response = "OK";
            response.messages = "Success";
            return Ok(response);
        }

        [Route("UpdateTodo")]
        [HttpPost]
        public IActionResult UpdateTodo([FromForm] UpdateTodoRequest param)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GetTodoResponse response = new GetTodoResponse();
            TodoModel todo = iTodoService.UpdateTodo(param);

            if (todo.TodoId == null)
            {
                response.response = "Not Found";
                response.messages = "Data Not Found";
                return NotFound(response);
            }

            response.todo = todo;

            response.response = "OK";
            response.messages = "Success";
            return Ok(response);
        }

        [Route("SetTodoPerc")]
        [HttpPost]
        public IActionResult SetTodoPerc([FromForm] SetPercentageRequest param)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GetTodoResponse response = new GetTodoResponse();
            TodoModel todo = iTodoService.SetTodoPercentage(param);

            if (todo.TodoId == null)
            {
                response.response = "Not Found";
                response.messages = "Data Not Found";
                return NotFound(response);
            }

            response.todo = todo;

            response.response = "OK";
            response.messages = "Success";
            return Ok(response);
        }

        [Route("DeleteTodo")]
        [HttpPost]
        public IActionResult DeleteTodo([FromForm] ByIdRequest param)
        {
            bool isDeleted = iTodoService.DeleteTodo(param);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("MarkTodoDone")]
        [HttpPost]
        public IActionResult MarkTodoDone([FromForm] ByIdRequest param)
        {
            GetTodoResponse response = new GetTodoResponse();
            TodoModel todo = iTodoService.MarkTodoDone(param);

            if (todo.TodoId == null)
            {
                response.response = "Not Found";
                response.messages = "Data Not Found";
                return NotFound(response);
            }

            response.todo = todo;

            response.response = "OK";
            response.messages = "Success";
            return Ok(response);
        }
    }
}
