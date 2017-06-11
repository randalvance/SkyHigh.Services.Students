using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkyHigh.Services.Students.Data;
using SkyHigh.Services.Students.Models;

namespace SkyHigh.Services.Students.Repositories
{
    public class StudentRepository
    {
        private StudentDbContext dbContext;
        
        public StudentRepository(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Student>> ListAsync()
        {
            return await dbContext.Students.ToAsyncEnumerable().ToList();
        }

        public async Task AddAsync(Student student)
        {
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int studentId)
        {
            var student = await dbContext.Students.FindAsync(studentId);
            dbContext.Remove(student);
            await dbContext.SaveChangesAsync();
        }
    }
}