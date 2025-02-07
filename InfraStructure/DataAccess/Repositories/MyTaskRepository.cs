using _01_Domain._01_Entities;
using _01_Domain._02_Contracts.Repositories;
using InfraStructure.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.DataAccess.Repositories
{
    public class MyTaskRepository : IMyTaskRepository
    {
        private readonly ToDoListDbContext _db;
        public MyTaskRepository(ToDoListDbContext db)
        {
            _db = db;
        }


        public async Task<int> GetIncompleteTaskCount(int userId, CancellationToken cancellationToken)
        {
            var result = await _db.Tasks.Where(t => t.UserId == userId && !t.IsCompleted)
                 .CountAsync(cancellationToken);
            return result;
        }

        public async Task Create(MyTask task, int userId, CancellationToken cancellationToken)
        {

            MyTask item = new()
            {
                UserId = userId,
                Title = task.Title,
                IsCompleted = false,
                CreatAt = DateTime.Now,
                Description = task.Description,

            };
            await _db.Tasks.AddAsync(item, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var item = await _db.Tasks.FindAsync(id, cancellationToken);
            _db.Tasks.Remove(item);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<MyTask>> GetAll(int userId, CancellationToken cancellationToken)
        {

            var items = await _db.Tasks.Where(t => t.UserId == userId)
              .ToListAsync(cancellationToken);
            return items;

        }

        public async Task MarkAsCompleted(int id, CancellationToken cancellationToken)
        {
            var item = await _db.Tasks.FindAsync(id, cancellationToken);
            item.IsCompleted = true;
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(MyTask model, CancellationToken cancellationToken)
        {

            var item = await _db.Tasks.FindAsync(model.Id, cancellationToken);
            item.IsCompleted = model.IsCompleted;
            item.Title = model.Title;
            item.Description = model.Description;
            await _db.SaveChangesAsync(cancellationToken);

        }

        public async Task<List<MyTask>> GetAllIncompleted(int userId, CancellationToken cancellationToken)
        {
            var items = await _db.Tasks
                 .Where(t => t.UserId == userId && !t.IsCompleted)
                 .Select(t => new MyTask
                 {
                     Id = t.Id,
                     Title = t.Title,
                     IsCompleted = t.IsCompleted,
                     CreatAt = t.CreatAt

                 })
                 .ToListAsync(cancellationToken);
            return items;
        }

     

        public async Task<MyTask> Get(int id, CancellationToken cancellationToken)
        {
            var item = await _db.Tasks.AsNoTracking()
                 .Where(t => t.Id == id)
                 .Select(t => new MyTask
                 {

                     Id = t.Id,
                     Title = t.Title,
                     IsCompleted = t.IsCompleted,
                     CreatAt = t.CreatAt
                 }).FirstOrDefaultAsync(cancellationToken); 
            return item;
        }
    }
}
