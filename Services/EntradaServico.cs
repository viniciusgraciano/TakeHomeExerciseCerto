using System;
using System.Collections.Generic;
using System.Text.Json;
using TakeHomeExercise.Entidades;

namespace TakeHomeExercise.Services
{
    public static class EntradaServico
    {
        public static void ProcessarEntrada(JsonSerializerOptions opcoesJson)
        {
            while (true)
            {
                var linha = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(linha))
                    break;

                var operacoes = JsonSerializer.Deserialize<List<Operacao>>(linha, opcoesJson)
                                ?? new List<Operacao>();

                var resultado = ProcessorService.ProcessarLinha(operacoes);

                var jsonSaida = JsonSerializer.Serialize(resultado, opcoesJson);
                Console.WriteLine(jsonSaida);
            }
        }
    }
}
