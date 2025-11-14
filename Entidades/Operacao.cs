using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TakeHomeExercise.Entidades
{
    public class Operacao
    {
        [JsonPropertyName("operation")]
        public string TipoOperacao { get; set; } = "";

        [JsonPropertyName("unit-cost")]
        public decimal CustoUnitario { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantidade { get; set; }
    }
}
