using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Irepo;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repo
{
    public class UserRepo: Iuser
    {
        private readonly AppDb _db;

        public UserRepo(AppDb db)
        {
            _db = db;
        }   
        public async Task<bool> AddUser(UserModel user)
        {
            await _db.Users.AddAsync(user);
            int rowsAffected = await _db.SaveChangesAsync();
            return rowsAffected > 0;
            // Here we are adding a new user and saving the changes in async mode unlike previous code
            // Now the benefit of async is in the following ways:
            // Async does not make your DB fast the operation will take same amount of time 
            // what async does is that it make the op non blocking i.e. when DB connection is happening 
            // the thread got released and used for other operation.
            // so better resource utilisation
        }
        public async Task<List<UserModel>> GetUsers()
        {
            List<UserModel> users = await _db.Users.ToListAsync();
            // is inside async method if we donot use await then
            // while DB is call is makde the thread will go on and return users as null
            // so calling await mens telling to wait to complete then only send;
            return users;
        }
    }
}
