using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domain.Data;
using ToDoList.Domain.Model;

namespace ToDoList.Services.ToDoService
{
    public class ToDoService : IToDoService
    {
        private readonly APIDBContext _context;
        public ToDoService(APIDBContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(ToDoItem newItem)
        {
            var entity = new ToDoItem
            {
                Id = Guid.NewGuid(),
                Title = newItem.Title,
                Description = newItem.Description,
                Complete = Convert.ToSingle(newItem.Complete),
                DueDate = Convert.ToDateTime(newItem.DueDate)
            };

            _context.ToDoItem.Add(entity);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> CompleteAsync(Guid id)
        {
            var item = await _context.ToDoItem.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item == null) return false;
            item.Complete = 100;
            var makeComplete = await _context.SaveChangesAsync();
            return makeComplete == 1;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var item = await _context.ToDoItem.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item == null) return false;
            _context.Remove(item);
            var makeComplete = await _context.SaveChangesAsync();
            return makeComplete == 1;
        }

        public async Task<ToDoItem> GetByIdAsync(Guid id)
        {
            var getbyid = await _context.ToDoItem.SingleOrDefaultAsync(x => x.Id == id);
            if (getbyid == null) getbyid = new ToDoItem();
            return getbyid;
        }

        public async Task<IEnumerable<ToDoItem>> IncomingTodo(string incomingtodo)
        {
            IEnumerable<ToDoItem> data = new List<ToDoItem>();
            if (incomingtodo.ToLower() == "today")
            {
                data = await _context.ToDoItem.Where(x => x.DueDate.Date == DateTime.Now.ToLocalTime().Date).ToArrayAsync();
            }
            else if (incomingtodo.ToLower() == "next day")
            {
                data = await _context.ToDoItem.Where(x => x.DueDate.Date == DateTime.Now.AddDays(1).ToLocalTime().Date).ToArrayAsync();
            }
            else if (incomingtodo.ToLower() == "current week")
            {
                data = await _context.ToDoItem.Where(x => x.DueDate.Date >= DateTime.Now.ToLocalTime().Date && x.DueDate.Date <= DateTime.Now.AddDays(7).ToLocalTime().Date).ToArrayAsync();
            }
            return data;
        }

        public async Task<IEnumerable<ToDoItem>> ListAsync()
        {
            var getall = _context.ToDoItem.ToArrayAsync();
            return await getall;
        }

        public async Task<bool> UpdateAsync(Guid id, ToDoItem todo)
        {
            var item = await _context.ToDoItem.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (item == null) return false;

            item.Title = todo.Title;
            item.Description = todo.Description;
            item.Complete = todo.Complete;
            item.DueDate = todo.DueDate;
            var saveUpdate = await _context.SaveChangesAsync();
            return saveUpdate == 1;
        }
    }
}
