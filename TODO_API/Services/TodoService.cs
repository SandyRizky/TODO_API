using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODO_API.DataAccess;
using TODO_API.Models;
using TODO_API.Models.Request;

namespace TODO_API.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _context;

        public TodoService(TodoDbContext context)
        {
            _context = context;
        }
        public List<TodoModel> getAllTodos()
        {
            List<TodoModel> todos = new List<TodoModel>();

            todos = _context.todo.ToList();

            return todos;
        }

        public TodoModel getTodoById(ByIdRequest param)
        {
            TodoModel todo = new TodoModel();

            todo = _context.todo.Where(x => x.TodoId == param.id).FirstOrDefault();

            return todo;
        }

        public List<TodoModel> getIncomingTodoToday()
        {
            List<TodoModel> todos = new List<TodoModel>();

            todos.AddRange(_context.todo.Where(x => x.ExpiredDate.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd")).ToList());  

            return todos;
        }

        public List<TodoModel> getIncomingTodoTomorrow()
        {
            List<TodoModel> todos = new List<TodoModel>();
            todos.AddRange(_context.todo.Where(x => x.ExpiredDate.ToString("yyyy-MM-dd") == DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")).ToList());

            return todos;
        }

        public List<TodoModel> getIncomingTodoThisWeek()
        {
            List<TodoModel> todos = new List<TodoModel>();

            DateTime todayDayStart = DateTime.Now;
            TimeSpan ts = new TimeSpan(0, 0, 0);
            todayDayStart = todayDayStart.Date + ts;

            todos.AddRange(_context.todo.Where(x => x.ExpiredDate.Date >= todayDayStart && x.ExpiredDate.Date <= GetNextWeekday(DateTime.Now, DayOfWeek.Sunday)).ToList());

            return todos;
        }

        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public TodoModel CreateTodo(CreateTodoRequest param)
        {
            TodoModel todo = new TodoModel();
            todo.Title = param.title;
            todo.Description = param.desc;
            todo.ExpiredDate = param.exp_date;
            todo.Percentage = param.perc;
            todo.CreatedBy = "User";
            todo.CreatedOn = DateTime.Now;
            todo.ModifiedBy = "User";
            todo.ModifiedOn = DateTime.Now;

            _context.todo.Add(todo);
            _context.SaveChanges();

            return todo;
        }

        public TodoModel UpdateTodo(UpdateTodoRequest param)
        {
            TodoModel todo = new TodoModel();
            todo = _context.todo.Where(x => x.TodoId == param.id).FirstOrDefault();

            if (todo.TodoId != null) {
                todo.Title = param.title;
                todo.Description = param.desc;
                todo.ExpiredDate = param.exp_date;
                todo.Percentage = param.perc;
                todo.ModifiedBy = "User";
                todo.ModifiedOn = DateTime.Now;

                _context.SaveChanges();
            }

            return todo;
        }

        public TodoModel SetTodoPercentage(SetPercentageRequest param)
        {
            TodoModel todo = new TodoModel();
            todo = _context.todo.Where(x => x.TodoId == param.id).FirstOrDefault();

            if (todo.TodoId != null)
            {
                todo.Percentage = param.perc;
                todo.ModifiedBy = "User";
                todo.ModifiedOn = DateTime.Now;

                _context.SaveChanges();
            }

            return todo;
        }

        public bool DeleteTodo(ByIdRequest param)
        {
            TodoModel todo = new TodoModel();
            todo = _context.todo.Where(x => x.TodoId == param.id).FirstOrDefault();

            if (todo is null)
            {
                return false;
            }

            _context.todo.Remove(todo);
            _context.SaveChanges();
            return true;
        }

        public TodoModel MarkTodoDone(ByIdRequest param)
        {
            TodoModel todo = new TodoModel();
            todo = _context.todo.Where(x => x.TodoId == param.id).FirstOrDefault();

            if (todo.TodoId != null)
            {
                todo.Percentage = 100;
                todo.ModifiedBy = "User";
                todo.ModifiedOn = DateTime.Now;

                _context.SaveChanges();
            }

            return todo;
        }
    }
}
