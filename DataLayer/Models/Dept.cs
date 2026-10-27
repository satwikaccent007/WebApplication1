using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace DataLayer.Models
{
    public  class Dept
    {
        [Key]
        public int DeptId { get; set; }
        public string DeptName { get; set; }

        List<Student> Students;
    }
}
