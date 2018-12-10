using Friends.Domain.Friend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Friends.Domain.Friend.Repositories
{
    public interface IFriendRepository
    {
        bool Create(FriendModel nome);
        List<FriendModel> Get();

    }
}
