using Microsoft.AspNetCore.Components;
using TodoList.API.Enums;
using TodoList.Model;
using TodoListBlazorWasm.Services;

namespace TodoListBlazorWasm.Pages
{
	public partial class TodoList
	{
		[Inject] private ITaskApiClient TaskApiClient { get; set; }
		private List<TaskDto> Tasks;
		private List<AssigneeDto> assignee;
		private TaskListSearch ListSearch = new TaskListSearch();
		protected override async Task OnInitializedAsync()
		{
			Tasks = await TaskApiClient.GetTaskList();
		}
		public class TaskListSearch
		{
			public string Name { get; set; }
			public Guid AssigneeId { get; set; }
			public Priority Priority { get; set; }
		}
	}
}
