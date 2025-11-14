using System;
using System.Collections.Generic;
using System.Text.Json;
using TakeHomeExercise.Entidades;



namespace TakeHomeExercise.Util
{
    /// <summary>
    /// Parâmetros de domínio (regras fixas) para cálculo de ganho de capital.
    /// Centraliza valores para facilitar manutenção e leitura.
    /// </summary>
    /// 
    public static class ParametrosTributarios
    {
        public const decimal PercentualImposto = 0.20m;
        public const decimal LimiteIsencao = 2000.00m;
    }

    public static class FormatadorDecimal
    {
        public static decimal Round2(decimal valor) =>
            Math.Round(valor, 2, MidpointRounding.AwayFromZero);
    }
}
