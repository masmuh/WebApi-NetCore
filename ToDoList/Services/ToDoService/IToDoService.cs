using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Domain.Model;

namespace ToDoList.Services.ToDoService
{
    public interface IToDoService
    {
        Task<IEnumerable<ToDoItem>> ListAsync();

        //get by id
        Task<ToDoItem> GetByIdAsync(Guid id);

        //get incoming todo
        Task<IEnumerable<ToDoItem>> IncomingTodo(string incomingtodo);


        //set complete
        Task<bool> CompleteAsync(Guid id);

        //create to do
        Task<bool> AddAsync(ToDoItem todo);

        //update to do
        Task<bool> UpdateAsync(Guid id, ToDoItem todo);

        //delete todo by id
        Task<bool> DeleteAsync(Guid id);
    }
}
