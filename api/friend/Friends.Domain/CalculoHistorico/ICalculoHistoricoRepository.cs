using Friends.Domain.CalculoHistorico.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Friends.Domain.CalculoHistorico
{
    public interface ICalculoHistoricoRepository
    {
        bool Create(CalculoHistoricoModel calculo);
    }
}
