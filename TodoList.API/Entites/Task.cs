using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoList.Model.Enums;

namespace TodoList.API.Entites
{
    public class Task
    {
        [Key]
        public Guid Id{ get; set; }
        public string NameType { get; set; }
        public Guid? AssigneeId { get; set; }
        [ForeignKey("AssigneeId")]
        public User Assignee { get; set; }
        public DateTime CreateDate{ get; set; }
        public Priority Priority{ get; set; }
        public Status Status{ get; set; }

    }
}
