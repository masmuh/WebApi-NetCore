using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domain.Model;

namespace ToDoList.Domain.Data
{
    public class APIDBContext : DbContext
    {
        public APIDBContext(DbContextOptions<APIDBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=dbtodo.db");

        public DbSet<ToDoItem> ToDoItem { get; set; }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<ToDoItem>().ToTable("ToDoItem");
        //    builder.Entity<ToDoItem>().HasKey(p => p.Id);
        //    builder.Entity<ToDoItem>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();//.HasValueGenerator<InMemoryIntegerValueGenerator<int>>();
        //    builder.Entity<ToDoItem>().Property(p => p.Title).IsRequired().HasMaxLength(30);

        //    builder.Entity<ToDoItem>().HasData
        //    (
        //        new ToDoItem { Id = new Guid(), Title = "testing", Description = "description testing", Complete = 30, DueDate = DateTime.Now.ToLocalTime() }
        //    );


        //}
    }
}
