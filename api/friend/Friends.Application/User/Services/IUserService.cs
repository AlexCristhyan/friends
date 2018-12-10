using Friends.Domain.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Application.User.Services
{
    public interface IUserService
    {
        UserModel Find(string name);
    }
}
