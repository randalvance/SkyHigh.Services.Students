using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkyHigh.Services.Students.Models;

namespace SkyHigh.Services.Students.Repositories
{
    // TODO: Use Entity Framework
    public class StudentRepository
    {
        private List<Student> students = new List<Student>
      {
            new Student
            {
                FirstName = "Randal",
                MiddleName = "Clanor",
                LastName = "Cunanan",
                DateOfBirth = new DateTime(1991, 8, 2)
            },
            new Student
            {
                FirstName = "Pablo",
                MiddleName = "Emilio",
                LastName = "Escobar",
                DateOfBirth = new DateTime(1949, 12, 1)
            }
      };

        public async Task<IEnumerable<Student>> ListAsync()
        {
            return await Task.FromResult<IEnumerable<Student>>(this.students);
        }

        public async Task AddAsync(Student student)
        {
            await Task.Run(() =>
            {
                this.students.Add(student);
            });
        }

        public async Task DeleteAsync(int studentId)
        {
            await Task.Run(() =>
            {
                var student = this.students.SingleOrDefault(x => x.StudentId == studentId);

                if (student != null)
                {
                    this.students.Remove(student);
                }
            });
        }
    }
}