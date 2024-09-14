using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.API.Enums;

namespace TodoList.Model
{
    public class TaskUpdateRequest
    {
        public string Name { get; set; }

        public Priority Priority { get; set; }
    }
}
