using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;
namespace DataLayer.Irepo
{
    public interface Iuser
    {
        Task<bool> CreateUser(UserModel user);
        Task<bool> UpdateUser(UserModel user);
        Task<List<UserModel>> GetUsers();
        Task<bool> DeleteUser(int id);
        Task<UserModel> GetUserById(int id);
        Task<bool> ValidateUser(string email,string password);
        Task<UserModel> GetUserByEmail(string email);
    }
}
