﻿using Portfolio.DB;
using Portfolio.DB.Models;
using Portfolio.Repository.Users.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portfolio.Repository.Users
{
    public interface IUsersRepository
    {
        Result Create(string userName, string password, string email);
        Result Authenticate(string userName, string password);
        Result Update(string userName, string password, string email, string newUserName = "", string newPassword = "", string newEmail = "");
        User GetBySid(string sid);
        User GetByUsername(string userName);
        List<User> GetAll();
    }
    public class UsersRepository : IUsersRepository
    {
        private readonly PortfolioDbContext _db;
        public UsersRepository(PortfolioDbContext db)
        {
            _db = db;
        }

        public Result Create(string userName, string password, string email)
        {
            var newUser = new User
            {
                UserName = userName,
                Password = password,
                Email = email,
                Sid = Guid.NewGuid().ToString(),
                //IsActive = true
            };

            try
            {
                _db.Users.Add(newUser);
                _db.SaveChanges();
                return new Result() { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new Result() { IsSuccess = false, Message = $"Failed to create new user: {ex.Message}" };
            }
        }

        public Result Authenticate(string userName, string password)
        {
            try
            {
                //var user = _db.Users.FirstOrDefault(x => (x.UserName == userName || x.Email == userName) && x.Password == password && x.IsActive == true);
                var user = _db.Users.FirstOrDefault(x => (x.UserName == userName || x.Email == userName) && x.Password == password);
                if (user == null)
                {
                    return new Result() { IsSuccess = false, Message = "User does not exist" };
                }
                else
                {
                    return new Result() { IsSuccess = true };
                }
            }
            catch (Exception ex)
            {
                return new Result() { IsSuccess = false, Message = $"Failed to retrieve user {ex.Message}" };
            }
        }

        public Result Update(string userName, string password, string email, string newUserName = "", string newPassword = "", string newEmail = "")
        {
            var user = _db.Users.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
            {
                return new Result() { IsSuccess = false, Message = "User doesn't exist" };
            }

            user.UserName = (newUserName != String.Empty || newUserName != null) ? newUserName : userName;
            user.Password = (newPassword != "" || newPassword != null) ? newPassword : password;
            user.Email = (newEmail != "" || newEmail != null) ? newEmail : email;

            try
            {
                _db.Update(user);
                _db.SaveChanges();
                return new Result() { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new Result() { IsSuccess = false, Message = $"Failed to update user data: {ex.Message}" };
            }
        }

        public User GetBySid(string sid)
        {
            try
            {
                //var user = _db.Users.FirstOrDefault(x => x.Sid == sid && x.IsActive == true);
                var user = _db.Users.FirstOrDefault(x => x.Sid == sid);
                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User GetByUsername(string userName)
        {
            try
            {
                //var user = _db.Users.FirstOrDefault(x => x.UserName == userName && x.IsActive == true);
                var user = _db.Users.FirstOrDefault(x => x.UserName == userName);
                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<User> GetAll()
        {
            try
            {
                var users = _db.Users.Where(x => x.Password != null).ToList();
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
