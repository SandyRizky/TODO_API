using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_API.Models.Request
{
    public class SetPercentageRequest
    {
        public Guid id { get; set; }
        public int perc { get; set; }
    }
}
