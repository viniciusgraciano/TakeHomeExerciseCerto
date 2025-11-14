using System;
using System.Collections.Generic;
using TakeHomeExercise.Entidades;

namespace TakeHomeExercise.Services
{
    public static class ProcessorService
    {
        public static List<TaxResultado> ProcessarLinha(IEnumerable<Operacao> operacoes)
        {
            var resultados = new List<TaxResultado>();
            var carteira = new Carteira();

            foreach (var op in operacoes)
            {
                if (op == null)
                {
                    resultados.Add(new TaxResultado { Taxa = 0m });
                    continue;
                }

                if (op.TipoOperacao.Equals("buy", StringComparison.OrdinalIgnoreCase))
                {
                    CarteiraServico.RegistrarCompra(carteira, op.CustoUnitario, op.Quantidade);
                    resultados.Add(new TaxResultado { Taxa = 0m });
                }
                else if (op.TipoOperacao.Equals("sell", StringComparison.OrdinalIgnoreCase))
                {
                    CarteiraServico.RegistrarVenda(
                        carteira,
                        op.CustoUnitario,
                        op.Quantidade,
                        out var _,
                        out var imposto
                    );

                    resultados.Add(new TaxResultado { Taxa = imposto });
                }
                else
                {
                    resultados.Add(new TaxResultado { Taxa = 0m });
                }
            }

            return resultados;
        }
    }
}
