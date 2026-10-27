using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models
{
    public class UserModel
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }  
        //here i have not writeen required still it is because defualt it is not null during model binding
        //to make it optional i need to do public string ? Name then it will be treated as not required

        // and if using [ApiController] then if not validated it will send 400
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Dob { get; set; }
        public int Age { get; set; }
    }
}
