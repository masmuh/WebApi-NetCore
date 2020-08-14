using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Model;
using Xunit;

namespace XUnitTestToDo
{
    public class FakeTest
    {
        public HttpClient TestClient { get; private set; }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [Fact]
        public async Task GetAll()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.GetAsync("api/todo/");
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetById()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.GetAsync("api/todo/905ebd0c-956b-4ec6-ae90-2ce389dd4cf6");
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetByIdErro()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.GetAsync("api/todo/905ebd0c-956b-4ec6-ae90-2ce389dd4cf4");
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task PutAction()
        {
            using (var client = new HttpClient())
            {
                var todoItem = new ToDoItem()
                {
                    Title = RandomString(8),
                    Complete = 50,
                    Description = RandomString(25),
                    DueDate = DateTime.Now.ToLocalTime()
                };
                
                var json = JsonConvert.SerializeObject(todoItem);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.PutAsync("api/todo/905ebd0c-956b-4ec6-ae90-2ce389dd4cf6", data);
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task AddAction()
        {
            using (var client = new HttpClient())
            {
                var todoItem = new ToDoItem()
                {
                    Title = RandomString(8),
                    Complete = 50,
                    Description = RandomString(25),
                    DueDate = DateTime.Now.ToLocalTime()
                };

                var json = JsonConvert.SerializeObject(todoItem);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.PostAsync("api/todo", data);
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }

    
}
