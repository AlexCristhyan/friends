using System;
using System.Collections.Generic;
using System.Text;

namespace Friends.Domain.Friend.Models
{
    public class FriendFullModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public int ResultDistancia { get; set; }
    }
}
