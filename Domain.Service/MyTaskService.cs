using _01_Domain._01_Entities;
using _01_Domain._02_Contracts.Repositories;
using _01_Domain._02_Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class MyTaskService : IMyTaskService
    {
        private readonly IMyTaskRepository _taskRepo;
        public MyTaskService(IMyTaskRepository taskRepo)
        {
            _taskRepo = taskRepo;
        }


        public async Task Create(MyTask task, int userId, CancellationToken cancellationToken)
        {
            await _taskRepo.Create(task, userId, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _taskRepo.Delete(id, cancellationToken);
        }

        public async Task<MyTask> Get(int id, CancellationToken cancellationToken)
        {
           return await _taskRepo.Get(id, cancellationToken);
        }

        public async Task<List<MyTask>> GetAll(int userId, CancellationToken cancellationToken)
        {
            return await _taskRepo.GetAll(userId, cancellationToken);
        }

        public async Task<List<MyTask>> GetAllIncompleted(int userId, CancellationToken cancellationToken)
        {
            return await _taskRepo.GetAllIncompleted(userId, cancellationToken);    
        }

        public async Task<int> GetIncompleteTaskCount(int userId, CancellationToken cancellationToken)
        {
            return await _taskRepo.GetIncompleteTaskCount(userId, cancellationToken);
        }

        public async Task MarkAsCompleted(int id, CancellationToken cancellationToken)
        {
            await _taskRepo.MarkAsCompleted(id, cancellationToken);
        }

        public async Task Update(MyTask model, CancellationToken cancellationToken)
        {
            await _taskRepo.Update(model, cancellationToken);
        }
    }
}
