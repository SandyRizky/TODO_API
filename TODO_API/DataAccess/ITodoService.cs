using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO_API.Models;
using TODO_API.Models.Request;

namespace TODO_API.DataAccess
{
    public interface ITodoService
    {
        List<TodoModel> getAllTodos();
        TodoModel getTodoById(ByIdRequest param);
        List<TodoModel> getIncomingTodoToday();
        List<TodoModel> getIncomingTodoTomorrow();
        List<TodoModel> getIncomingTodoThisWeek();
        TodoModel CreateTodo(CreateTodoRequest param);
        TodoModel UpdateTodo(UpdateTodoRequest param);
        TodoModel SetTodoPercentage(SetPercentageRequest param);
        bool DeleteTodo(ByIdRequest param);
        TodoModel MarkTodoDone(ByIdRequest param);
    }
}
