using DataLayer.Models;
using DataLayer.Irepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repos
{
    public class SchoolRepo : Ischool
    {
        private readonly AppDb _db;

        public SchoolRepo(AppDb _db)
        {
            this._db = _db;
        }
        public async Task<List<Student>> GetStudents()
        {
            List<Student> students = await _db.Student.Include(e => e.Dept).ToListAsync();
            return students;
        }
        public async Task<List<Dept>> GetDepartments()
        {
            return new List<Dept>();
        }
        public async Task<bool> CreateStudent()
        {
            return true;
        }
        public async Task<bool> CreateDept()
        {
            return true;
        }
    }
}
