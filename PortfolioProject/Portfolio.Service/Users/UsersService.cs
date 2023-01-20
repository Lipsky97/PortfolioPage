using Portfolio.Repository.Users.Model;
using Portfolio.Repository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Service.Users
{
    public class UsersService
    {
        private readonly UsersRepository _usersRepository;
        public UsersService(UsersRepository usersRepository) 
        { 
            _usersRepository = usersRepository; 
        }

        public Result Create(string userName, string password, string email)
        {
            var result = _usersRepository.Create(userName, password, email);
            return result;
        }
    }
}
