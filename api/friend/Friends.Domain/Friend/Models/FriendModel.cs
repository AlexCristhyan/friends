using System;
using System.Collections.Generic;
using System.Text;

namespace Friends.Domain.Friend.Models
{
    public class FriendModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
    }
}

