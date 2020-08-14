using System;

namespace ToDoList.Domain.Model
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Complete { get; set; }
        public DateTime DueDate { get; set; }
    }
}
