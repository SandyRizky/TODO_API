using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_API.Models.Request
{
    public class CreateTodoRequest
    {
        public string title { get; set; }
        public string desc { get; set; }
        public int perc { get; set; }
        public DateTime exp_date { get; set; }
    }
}
