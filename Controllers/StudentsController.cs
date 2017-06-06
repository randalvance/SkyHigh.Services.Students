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
    }
}