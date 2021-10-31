using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TODO_API.DataAccess;
using TODO_API.Models;
using TODO_API.Models.Request;

namespace TODO_API_NUNIT_TEST
{
    public class TodoServiceFake : ITodoService
    {
        private readonly List<TodoModel> _todoList;

        public TodoServiceFake(){
            _todoList = new List<TodoModel>()
            {
                new TodoModel() {
                    TodoId = new Guid("cb1fd412-78cd-4cf3-a80a-67ea0cd9b211"),
                    Title = "Grocery List",
                    Description="1. Milk, 2. Eggs, 3.Butter",
                    ExpiredDate = Convert.ToDateTime("29/10/2021"),
                    Percentage = 0,
                    CreatedBy = "User",
                    CreatedOn = Convert.ToDateTime("28/10/2021"),
                    ModifiedBy = "User",
                    ModifiedOn = Convert.ToDateTime("28/10/2021")
                },
                new TodoModel() {
                    TodoId = new Guid("ad1fd434-78cd-4cf3-a80a-67ea0cd9b322"),
                    Title = "Homework",
                    Description="1. Chemistry, 2. Math",
                    ExpiredDate = Convert.ToDateTime("30/10/2021"),
                    Percentage = 0,
                    CreatedBy = "User",
                    CreatedOn = Convert.ToDateTime("28/10/2021"),
                    ModifiedBy = "User",
                    ModifiedOn = Convert.ToDateTime("28/10/2021")
                }
            };
        }

        public TodoModel CreateTodo(CreateTodoRequest param)
        {
            TodoModel todo = new TodoModel();
            todo.TodoId = new Guid();
            todo.Title = param.title;
            todo.Description = param.desc;
            todo.ExpiredDate = param.exp_date;
            todo.Percentage = param.perc;
            todo.CreatedBy = "User";
            todo.CreatedOn = DateTime.Now;
            todo.ModifiedBy = "User";
            todo.ModifiedOn = DateTime.Now;

            _todoList.Add(todo);

            return todo;
        }

        public bool DeleteTodo(ByIdRequest param)
        {
            TodoModel todo = new TodoModel();
            todo = _todoList.Where(x => x.TodoId == param.id).FirstOrDefault();

            if (todo is null)
            {
                return false;
            }

            _todoList.Remove(todo);
            return true;
        }

        public List<TodoModel> getAllTodos()
        {

            List<TodoModel> todos = new List<TodoModel>();

            todos = _todoList.ToList();

            return todos;
        }

        public List<TodoModel> getIncomingTodoThisWeek()
        {
            List<TodoModel> todos = new List<TodoModel>();

            DateTime todayDayStart = DateTime.Now;
            TimeSpan ts = new TimeSpan(0, 0, 0);
            todayDayStart = todayDayStart.Date + ts;

            todos.AddRange(_todoList.Where(x => x.ExpiredDate.Date >= todayDayStart && x.ExpiredDate.Date <= GetNextWeekday(DateTime.Now, DayOfWeek.Sunday)).ToList());

            return todos;
        }

        public List<TodoModel> getIncomingTodoToday()
        {
            List<TodoModel> todos = new List<TodoModel>();

            todos.AddRange(_todoList.Where(x => x.ExpiredDate.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd")).ToList());

            return todos;
        }

        public List<TodoModel> getIncomingTodoTomorrow()
        {
            List<TodoModel> todos = new List<TodoModel>();
            todos.AddRange(_todoList.Where(x => x.ExpiredDate.ToString("yyyy-MM-dd") == DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")).ToList());

            return todos;
        }

        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public TodoModel getTodoById(ByIdRequest param)
        {
            TodoModel todo = new TodoModel();

            todo = _todoList.Where(x => x.TodoId == param.id).FirstOrDefault();

            return todo;
        }

        public TodoModel MarkTodoDone(ByIdRequest param)
        {
            TodoModel todo = new TodoModel();
            todo = _todoList.Where(x => x.TodoId == param.id).FirstOrDefault();
            if (todo.TodoId != null)
            {
                todo.Percentage = 100;
            }

            return todo;
        }

        public TodoModel SetTodoPercentage(SetPercentageRequest param)
        {
            TodoModel todo = new TodoModel();
            todo = _todoList.Where(x => x.TodoId == param.id).FirstOrDefault();
            if (todo.TodoId != null)
            {
                todo.Percentage = param.perc;
            }

            return todo;
        }

        public TodoModel UpdateTodo(UpdateTodoRequest param)
        {
            TodoModel todo = new TodoModel();
            todo = _todoList.Where(x => x.TodoId == param.id).FirstOrDefault();
            if (todo.TodoId != null) {
                todo.Title = param.title;
                todo.Description = param.desc;
                todo.Percentage = param.perc;
                todo.ExpiredDate = param.exp_date;
            }

            return todo;
        }
    }
}
