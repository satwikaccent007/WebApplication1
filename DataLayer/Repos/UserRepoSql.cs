using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Data.SqlClient;
using DataLayer.Irepo;
using DataLayer.Models;

namespace DataLayer.Repos
{
    public  class UserRepoSql:Iuser
    {
        private readonly AppDb _db;

        public UserRepoSql(AppDb db)
        {
            this._db = db;
        }
        public async Task<bool> CreateUser(UserModel user)
        {
            string sp = "exec createUser @Name,@Email,@Password,@Dob,@Age";
            List<SqlParameter> param = new List<SqlParameter>(){
                new SqlParameter("@Name",user.Name),
                new SqlParameter("@Email",user.Email),
                new SqlParameter("@Password",user.Password),
                new SqlParameter("@Dob",user.Dob),
                new SqlParameter("@Age",user.Age),
            };

            int res = await _db.Database.ExecuteSqlRawAsync(sp, param.ToArray());
            return res>0;
        }
        public async Task<bool> UpdateUser(UserModel user)
        {
            string sp = "exec UpdateUser @ID,@Name,@Email,@Password,@Dob,@Age";
            List<SqlParameter> param = new List<SqlParameter>(){
                new SqlParameter("@ID",user.ID),
                new SqlParameter("@Name",user.Name),
                new SqlParameter("@Email",user.Email),
                new SqlParameter("@Password",user.Password),
                new SqlParameter("@Dob",user.Dob),
                new SqlParameter("@Age",user.Age),
            };

            int result = await  _db.Database.ExecuteSqlRawAsync(sp, param.ToArray());
            return result > 0;
        }
        public async Task<List<UserModel>> GetUsers()
        {
            string sp = "Exec getUsers";
            List<UserModel> users = await _db.User.FromSqlRaw(sp).ToListAsync();
            //FromSqlRaw is used for get method for getting data
            // and generally data comes in the form of List;
            return users;
        }
        public async Task<bool> DeleteUser(int id)
        {
            String sp = "Exec DeleteUser @ID";
            SqlParameter param = new SqlParameter("@ID", id);
            int res = await _db.Database.ExecuteSqlRawAsync(sp, param); 
            //ExecuteSqlRaw is mainy for post method 
            // Also it doesnot return data it returns rows effected                                               
            return res > 0;
        }
        public async Task<UserModel> GetUserById(int id)
        {

            string sp = "Exec GetUserById @ID";
            SqlParameter param = new SqlParameter("@ID",id);

            List<UserModel> users = await _db.User.FromSqlRaw(sp, param).ToListAsync();
            return users.FirstOrDefault();
        }
        public async Task<bool> ValidateUser(string email, string password)
        {
            string sp = "Exec ValidateUser @Email,@Password";
            List<SqlParameter> param = new List<SqlParameter>
            {
                new SqlParameter("@Email",email),
                new SqlParameter("@password",password)
            };
            List<UserModel> users = await _db.User.FromSqlRaw(sp, param.ToArray()).ToListAsync();
            UserModel user = users.FirstOrDefault();

            return user != null;
        }
        public async Task<UserModel> GetUserByEmail(string email)
        {
            UserModel user = await _db.User.SingleOrDefaultAsync((user) => user.Email == email);
            return user;
        }
    }
}
