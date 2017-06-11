using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
            // TODO: this should be in the startup, but it needs to wait for the postgre container to fully start up
            // We need to call a script that probes the postgre container first before running migrations
            // for more info: https://github.com/vishnubob/wait-for-it
            // as a hack for now, let's call migrate when list was called
            await dbContext.Database.MigrateAsync();
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