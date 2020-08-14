using Microsoft.AspNetCore.Mvc;
using System;
using ToDoList.Domain.Model;
using ToDoList.Services.ToDoService;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _todoItemService;
        public ToDoController(IToDoService todoItemService)
        {
            _todoItemService = todoItemService;
        }
        //http://localhost:5000/api/todo
        // GET all task todo
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var get = _todoItemService.ListAsync();
                return Ok(get);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        // GET by id
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            try
            {
                var parseId = Guid.Parse(id);
                var get = _todoItemService.GetByIdAsync(parseId);
                return Ok(get);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [Route("api/todo/incoming")]
        [HttpGet("incoming/{id}/")]
        public ActionResult Incoming(string id)
        {
            try
            {
                var inc = _todoItemService.IncomingTodo(id);
                return Ok(inc);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [Route("api/todo/complete")]
        [HttpPut("complete/{id}")]
        public ActionResult Complete(string id)
        {
            try
            {
                var inc = _todoItemService.CompleteAsync(Guid.Parse(id));
                return Ok(inc);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }


        //add new task todo
        [HttpPost]
        public IActionResult Post([FromBody] ToDoItem data)
        {
            try
            {
                var create = _todoItemService.AddAsync(data);
                return Ok(create);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        // PUT update task todo
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] ToDoItem value)
        {
            try
            {
                var update = _todoItemService.UpdateAsync(Guid.Parse(id), value);
                return Ok(update);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        // DELETE by id
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var delete = _todoItemService.DeleteAsync(Guid.Parse(id));
                return Ok(delete);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }
    }
}
