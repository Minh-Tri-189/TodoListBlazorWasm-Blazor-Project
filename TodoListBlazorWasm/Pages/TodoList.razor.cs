using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TodoList.Model;
using TodoList.API.Enums;
using TodoListBlazorWasm.Services;

namespace TodoListBlazorWasm.Pages
{
	public partial class TodoList
	{
		[Inject] private ITaskApiClient TaskApiClient { get; set; }
        [Inject] private IUserApiClient UserApiClient{ get; set; }
        private List<TaskDto> Tasks;
		private List<AssigneeDto> Assignee;
		private TaskListSearch TaskListSearch = new TaskListSearch();
		protected override async Task OnInitializedAsync()
		{
			Tasks = await TaskApiClient.GetTaskList(TaskListSearch);
            Assignee = await UserApiClient.GetAssignee();
        }
		private async Task SearchForm(EditContext context)
		{
			Tasks = await TaskApiClient.GetTaskList(TaskListSearch);
		}
		
	}
}
