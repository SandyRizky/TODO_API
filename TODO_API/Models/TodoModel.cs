using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_API.Models
{
    public class TodoModel
    {
        [Key]
        public Guid TodoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Percentage { get; set; } 
        public DateTime ExpiredDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
