using _01_Domain._01_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Domain._02_Contracts.Services
{
    public interface IMyTaskService
    {
        Task Create(MyTask task, int userId, CancellationToken cancellationToken);
        Task<List<MyTask>> GetAll(int userId, CancellationToken cancellationToken);
        Task<List<MyTask>> GetAllIncompleted(int userId, CancellationToken cancellationToken);

        Task Update(MyTask model, CancellationToken cancellationToken);
        Task MarkAsCompleted(int id, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<int> GetIncompleteTaskCount(int userId, CancellationToken cancellationToken);
        Task<MyTask> Get(int id, CancellationToken cancellationToken);


    }
}
