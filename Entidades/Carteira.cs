using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TakeHomeExercise.Entidades
{
    public class Carteira
    {
        public int Quantidade { get; internal set; }
        public decimal CustoMedio { get; internal set; }
        public decimal PrejuizoAcumulado { get; internal set; }
    }
}

