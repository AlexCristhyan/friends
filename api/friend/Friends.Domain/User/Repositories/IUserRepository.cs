using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Domain.User.Repositories
{
    public interface IUserRepository
    {
        UserModel Find(string nome);
    }
}
