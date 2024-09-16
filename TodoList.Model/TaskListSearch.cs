using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Model.Enums;
using TodoList.Model.SeedWork;

namespace TodoList.Model
{
    public class TaskListSearch : PagingParameters
    {
        public string? NameType { get; set; }
        public Guid? AssigneeId { get; set; }
        public Priority? Priority { get; set; }
    }
}
