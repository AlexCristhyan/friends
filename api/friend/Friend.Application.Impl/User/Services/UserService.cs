using Friends.Application.User.Services;
using Friends.Domain.User;
using Friends.Domain.User.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Application.Impl.User.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public UserModel Find(string name)
        {
            return _repository.Find(name);
        }
    }
}
