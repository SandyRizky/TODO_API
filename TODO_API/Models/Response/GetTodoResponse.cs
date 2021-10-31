using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_API.Models.Response
{
    public class GetTodoResponse
    {
        public TodoModel todo { get; set; }
        public string response { get; set; }
        public string messages { get; set; }
    }
}
