using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;

namespace DataLayer.Irepo
{
    public interface Ischool
    {
        Task<List<Student>> GetStudents();
        Task<List<Dept>> GetDepartments();
        Task<bool> CreateStudent();
        Task<bool> CreateDept();
    }
}
