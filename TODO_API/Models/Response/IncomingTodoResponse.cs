using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_API.Models.Response
{
    public class IncomingTodoResponse
    {
        public List<TodoModel> today_todo { get; set; }
        public List<TodoModel> tomorrow_todo { get; set; }
        public List<TodoModel> this_week_todo { get; set; }
        public string response { get; set; }
        public string messages { get; set; }
    }
}
