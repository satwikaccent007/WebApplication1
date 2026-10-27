using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Irepo
{
    public interface Iuser
    {
        Task<bool>  AddUser(UserModel user);
        Task<List<UserModel>> GetUsers();
    }
}
