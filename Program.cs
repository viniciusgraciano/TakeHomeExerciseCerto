using System;
using System.Text.Json;
using TakeHomeExercise.Services;

namespace TakeHomeExercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && string.Equals(args[0], "json", StringComparison.OrdinalIgnoreCase))
            {
                var opcoesJson = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                EntradaServico.ProcessarEntrada(opcoesJson);
            }
            else
            {
                ModoInterativoConsole.Executar();
            }
        }
    }
}
