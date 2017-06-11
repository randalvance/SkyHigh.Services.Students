using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SkyHigh.Services.Students.Models;
using SkyHigh.Services.Students.Repositories;

namespace SkyHigh.Services.Students.Controllers
{
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private StudentRepository studentRepository;

        public StudentsController(StudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await this.studentRepository.ListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Student student)
        {
            await this.studentRepository.AddAsync(student);

            // TODO: retrieve hostname from environment variable
            var factory = new ConnectionFactory() { HostName = "rabbitmq" /* name of docker service */ };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "students", type: "fanout");

                string json = JsonConvert.SerializeObject(student);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "students", routingKey: "", basicProperties: null, body: body);
            }

            return this.Created("", student); // TODO: generate resource link
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int studentId)
        {
            await this.studentRepository.DeleteAsync(studentId);

            return this.Ok();
        }
    }
}