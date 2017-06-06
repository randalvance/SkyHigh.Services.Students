using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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