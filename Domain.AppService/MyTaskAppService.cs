using _01_Domain._01_Entities;
using _01_Domain._02_Contracts.AppServices;
using _01_Domain._02_Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AppService
{
    public class MyTaskAppService : IMyTaskAppService
    {
        public IMyTaskService _taskService { get; }
        public MyTaskAppService(IMyTaskService taskService)
        {
            _taskService = taskService;
        }

        

        public async Task Create(MyTask task, int userId, CancellationToken cancellationToken)
        {
            await _taskService.Create(task, userId, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _taskService.Delete(id, cancellationToken);   
        }

        public async Task<MyTask> Get(int id, CancellationToken cancellationToken)
        {
            return await _taskService.Get(id, cancellationToken);
        }

        public async Task<List<MyTask>> GetAll(int userId, CancellationToken cancellationToken)
        {
            return await _taskService.GetAll(userId, cancellationToken);
        }

        public async Task<List<MyTask>> GetAllIncompleted(int userId, CancellationToken cancellationToken)
        {
            return await _taskService.GetAllIncompleted(userId, cancellationToken);
        }

        public async Task<int> GetIncompleteTaskCount(int userId, CancellationToken cancellationToken)
        {
            return await _taskService.GetIncompleteTaskCount(userId, cancellationToken);
        }

        public async Task MarkAsCompleted(int id, CancellationToken cancellationToken)
        {
            await _taskService.MarkAsCompleted(id, cancellationToken);
        }

        public async Task Update(MyTask model, CancellationToken cancellationToken)
        {
            await _taskService.Update(model, cancellationToken);
        }
    }
}
