using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DataLayer.Models;
using DataLayer.Irepo;
using Microsoft.EntityFrameworkCore;
namespace DataLayer.Repos
{
    public  class UserRepo : Iuser
    {
        private readonly AppDb _db;
        public UserRepo(AppDb db) {
            this._db = db;
        }

        public async Task<bool> CreateUser(UserModel user)
        {
            try
            {
                if (user == null) throw new Exception("Data not given");
                if (user.Name == null) throw new Exception("Name is required");
                if (user.Email == null) throw new Exception("Email is required");
                if (user.Password == null) throw new Exception("Password is required");


                string name = user.Name.Trim();
                string email = user.Email.Trim();
                string passwd = user.Password.Trim();

                bool emailExist = await _db.User.AnyAsync((user) => user.Email == email);
                if (emailExist) throw new Exception("Email already exists");

                var myData = new UserModel
                {
                    Name = name,
                    Email = email,
                    Password = passwd,
                    Dob = user.Dob,
                    Age = user.Age,
                };

                await _db.User.AddAsync(myData);
                int result = await _db.SaveChangesAsync();

                return result > 0;

            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<UserModel> GetUserById(int id)
        {
            UserModel user = await _db.User.FindAsync(id);
            if (user == null) throw new Exception("User Not Found");
            return user;
        }
        public async Task<bool> UpdateUser(UserModel user)
        {
            try
            {
                if (user == null) throw new Exception("Data not given");
                if (user.Name == null) throw new Exception("Name is required");
                if (user.Email == null) throw new Exception("Email is required");
                if (user.Password == null) throw new Exception("Password is required");

                string name = user.Name.Trim();
                string email = user.Email.Trim();
                string passwd = user.Password.Trim();

                UserModel existUser = await GetUserById(user.ID);
                if(existUser.Email != user.Email)
                {
                    bool emailExist = await _db.User.AnyAsync((user) => user.Email == email);
                    if(emailExist) throw new Exception("email already exists");
                }

                existUser.Name = name;
                existUser.Email = email;
                existUser.Password = passwd;
                existUser.Dob = user.Dob;
                existUser.Age = user.Age;
                int result = await _db.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // In edit first thing if you donot edit then it will throw an error
        // we must make some change or else handle that condition
        // secondly when ever we call existUser the object is tracked by EFcore for chnages
        // now if we create a new then that also have same id i.e. primary key so its best to make
        // the chnages in existUser and perform SaveChnages()
        // else we can do .AsNoTracking() while calling it then it will not be tracked 

        public async Task<List<UserModel>> GetUsers()
        {
            List<UserModel> users = await _db.User.ToListAsync();
            return users;
        }

        public async Task<bool> DeleteUser(int id)
        {
            UserModel existUser = await GetUserById(id);
            if (existUser != null)
            {
                _db.User.Remove(existUser);
                int result = await _db.SaveChangesAsync();
                return (result > 0);
            }
            else throw new Exception("User with this id does not exist");

            
        }
        public async Task<bool> ValidateUser(string email,string password)
        {
            bool result = await _db.User.AnyAsync((user) => user.Email == email && user.Password == password);
            return result;
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            UserModel user = await _db.User.SingleOrDefaultAsync((user) => user.Email == email);
            return user;
        }
    }
}
