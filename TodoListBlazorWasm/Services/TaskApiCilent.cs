
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoList.Model;
using TodoList.Model.SeedWork;

namespace TodoListBlazorWasm.Services
{
    public class TaskApiClient : ITaskApiClient
    {
        public HttpClient _httpClient;

        public TaskApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AssignTask(Guid id, AssignTaskRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/task/{id}/assign", request);
            return result.IsSuccessStatusCode;
        }


        public async Task<bool> CreateTask(TaskCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Task", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTask(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/Task/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<PagedList<TaskDto>> GetMyTasks(TaskListSearch taskListSearch)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = taskListSearch.PageNumber.ToString()
            };

            if (!string.IsNullOrEmpty(taskListSearch.NameType))
                queryStringParam.Add("name", taskListSearch.NameType);
            if (taskListSearch.AssigneeId.HasValue)
                queryStringParam.Add("assigneeId", taskListSearch.AssigneeId.ToString());
            if (taskListSearch.Priority.HasValue)
                queryStringParam.Add("priority", taskListSearch.Priority.ToString());

            string url = QueryHelpers.AddQueryString("/api/Task/me", queryStringParam);

            var result = await _httpClient.GetFromJsonAsync<PagedList<TaskDto>>(url);
            return result;
        }

        public async Task<TaskDto> GetTaskDetail(string id)
        {
            var result = await _httpClient.GetFromJsonAsync<TaskDto>($"/api/Task/{id}");
            return result;
        }

        public async Task<PagedList<TaskDto>> GetTaskList(TaskListSearch taskListSearch)
        {
          
            var queryStringParam = new Dictionary<string, string> {
                ["pageNumber"] = taskListSearch.PageNumber.ToString()
            };

            if (!string.IsNullOrEmpty(taskListSearch.NameType))
                queryStringParam.Add("Name", taskListSearch.NameType);

            if (taskListSearch.AssigneeId.HasValue)
                queryStringParam.Add("AssignId", taskListSearch.Priority.ToString());

            if (taskListSearch.Priority.HasValue)
                queryStringParam.Add("Priority", taskListSearch.Priority.ToString());

            string url = QueryHelpers.AddQueryString("api/Task", queryStringParam);
                //$"/api/Task?NameType={taskListSearch.NameType}&AssigneeId={taskListSearch.AssigneeId}&Priority={taskListSearch.Priority}";

            var result = await _httpClient.GetFromJsonAsync<PagedList<TaskDto>>(url);
            return result;
        }

        public async Task<bool> UpdateTask(Guid id, TaskUpdateRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/Task/{id}", request);
            return result.IsSuccessStatusCode;

        }
    }
}