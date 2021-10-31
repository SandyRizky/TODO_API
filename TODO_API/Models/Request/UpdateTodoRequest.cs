using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_API.Models.Request
{
    public class UpdateTodoRequest
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public int perc { get; set; }
        public DateTime exp_date { get; set; }
    }
}
