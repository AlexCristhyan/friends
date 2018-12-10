using System;
using System.Collections.Generic;
using System.Text;

namespace Friends.Domain.CalculoHistorico.Models
{
    public class CalculoHistoricoModel
    {
        public int Id { get; set; }
        public int DiferencaLatitude { get; set; }
        public int DiferencaLongitude { get; set; }
        public int ResultadoDistancia { get; set; }
        public DateTime DataCalculo { get; set; }
        public int FriendId { get; set; }
    }
}
