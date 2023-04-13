using Portfolio.Repository.Users;
using Portfolio.Repository.Users.Model;
using Portfolio.Service.Users.Model;
using System;
using System.Collections.Generic;

namespace Portfolio.Service.Users
{
    public interface IUsersService
    {
        bool Create(string userName, string password, string email);
        bool Authenticate(string userName, string password);
        Result Update(string userName, string password, string email, string newUsername, string newPassword, string newEmail);
        UserVD GetBySid(string sid);
        UserVD GetByUsername(string userName);
        List<UserVD> GetAll();
        Result Delete(string sid);
    }

    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository) 
        { 
            _usersRepository = usersRepository; 
        }

        public bool Create(string userName, string password, string email)
        {
            var result = _usersRepository.Create(userName, password, email);
            return result.IsSuccess;
        }

        public bool Authenticate(string userName, string password)
        {
            var result = _usersRepository.Authenticate(userName, password);
            return result.IsSuccess;
        }

        public Result Update(string userName, string password, string email, string newUsername, string newPassword, string newEmail) 
        {
            var result = _usersRepository.Update(userName, password, email, newUsername, newPassword, newEmail);
            return result;
        }

        public UserVD GetBySid(string sid)
        {
            var userEntity = _usersRepository.GetBySid(sid);
            var result = new UserVD()
            {
                Sid = new Guid(userEntity.Sid),
                UserName = userEntity.UserName,
                Email= userEntity.Email,
                ProfilePic = userEntity.ProfilePic
            };
            return result;
        }

        public Result Delete(string sid)
        {
            return _usersRepository.Delete(sid);
        }

        public UserVD GetByUsername(string userName)
        {
            var userEntity = _usersRepository.GetByUsername(userName);
            var result = new UserVD()
            {
                Sid = new Guid(userEntity.Sid),
                UserName = userEntity.UserName,
                Email = userEntity.Email,
                ProfilePic = userEntity.ProfilePic
            };
            return result;
        }

        public List<UserVD> GetAll()
        {
            var result = new List<UserVD>();
            var users = _usersRepository.GetAll();
            foreach (var user in users)
            {
                var r = new UserVD()
                {
                    Email = user.Email,
                    //IsActive = user.IsActive,
                    UserName = user.UserName,
                    Password = user.Password,
                    ProfilePic = user.ProfilePic,
                    Sid = new Guid(user.Sid)
                };
                result.Add(r);
            }

            return result;
        }
    }
}
