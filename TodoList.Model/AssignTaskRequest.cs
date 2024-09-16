namespace TodoList.Model
{
    public class AssignTaskRequest
    {
        public Guid? UserId { get; set; }
        public Guid TaskId { get; set; }
    }
}
