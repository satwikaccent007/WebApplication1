using DataAccessLayer.Irepo;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repo
{
    public class UserTestRepo:Iuser
    {
        public async Task<bool> AddUser(UserModel user)
        {
            return true;
        }
        public async Task<List<UserModel>> GetUsers()
        {
            List<UserModel> users = new List<UserModel>();
            users.Add(new UserModel
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com"
            });
            users.Add(new UserModel
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "jane.smith@example.com"
            });
            return users;
        }
    }
}


// Now the purpose of this class is to provide a test implementation without connection to the DB
// Now is the reason we are using a interface instid of contrete classes
// using DI we can easily switch between the two implementations without changing the code in controller
