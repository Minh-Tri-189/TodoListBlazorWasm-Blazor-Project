﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TodoList.Model;
using TodoList.Model.Enums;
using TodoListBlazorWasm.Services;
using Blazored.Toast.Services;
using TodoListBlazorWasm.Component;
using TodoListBlazorWasm.Pages.Component;
using TodoList.Model.SeedWork;
using TodoListBlazorWasm.Shared;

namespace TodoListBlazorWasm.Pages
{
    public partial class TodoList
    {
        [Inject] private ITaskApiClient TaskApiClient { get; set; }


        private List<TaskDto> Tasks;

        private TaskListSearch TaskListSearch = new TaskListSearch();
        protected Confirmation DeleteConfirmation { set; get; }
        public MetaData MetaData { get; set; } = new MetaData();
        protected AssignTask AssignTaskDialog{ set; get; }
        [CascadingParameter]
        private Error error { set; get; }
        private Guid DeleteId { set; get; }
        protected override async Task OnInitializedAsync()
        {
           
            await GetTasks();
        }
        public async Task SearchTask(TaskListSearch taskListSearch)
        {
            TaskListSearch = taskListSearch;
            await GetTasks();
        }
        public async Task OndeleteTask(Guid deleteId)
        {
            DeleteId = deleteId;
            DeleteConfirmation.Show();
        }
        public async Task OnConfirmDeleteTask(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await TaskApiClient.DeleteTask(DeleteId);
                await GetTasks();

            }
        }
        public void OpenAssignPopup(Guid Id)
        {
            AssignTaskDialog.Show(Id);
        }
        public async Task AssignTaskSuccess(bool result)
        {
            if (result)
            {
                await GetTasks();
            }
        }
        private async Task GetTasks()
        {
            try {
                var pagingResponse = await TaskApiClient.GetMyTasks(TaskListSearch);

                Tasks = pagingResponse.Items;

                MetaData = pagingResponse.MetaData;
            }
            catch (Exception ex) { 
            error.ProcessError(ex);
            }
        }
        private async Task SelectedPage(int page)
        {
            TaskListSearch.PageNumber = page;
            await GetTasks();
        }
    }
}
