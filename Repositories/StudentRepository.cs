using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkyHigh.Services.Students.Models;

namespace SkyHigh.Services.Students.Repositories
{
  public class StudentRepository
  {
      public async Task<IEnumerable<Student>> ListAsync()
      {
          return await Task.FromResult<IEnumerable<Student>>(
              new List<Student>()
              {
                  new Student
                  {
                      FirstName = "Randal",
                      MiddleName = "Clanor",
                      LastName = "Cunanan",
                      DateOfBirth = new DateTime(1991, 8, 2)
                  }
              }
          );
      }
  }
}