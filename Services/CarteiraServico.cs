using TakeHomeExercise.Entidades;
using TakeHomeExercise.Util;

namespace TakeHomeExercise.Services
{
    public static class CarteiraServico
    {
        public static void RegistrarCompra(Carteira carteira, decimal custoUnitario, int quantidade)
        {
            var custoTotalAnterior = carteira.CustoMedio * carteira.Quantidade;
            var custoTotalNovo = custoUnitario * quantidade;
            var novaQuantidade = carteira.Quantidade + quantidade;

            if (novaQuantidade > 0)
            {
                carteira.CustoMedio = FormatadorDecimal.Round2(
                    (custoTotalAnterior + custoTotalNovo) / novaQuantidade
                );
            }

            carteira.Quantidade = novaQuantidade;
        }

        public static void RegistrarVenda(Carteira carteira,decimal custoUnitario,int quantidade,out decimal lucro,out decimal imposto)
        {
            lucro = FormatadorDecimal.Round2(
                (custoUnitario - carteira.CustoMedio) * quantidade
            );

            var valorTotal = custoUnitario * quantidade;
            imposto = 0m;

            var isento = valorTotal <= ParametrosTributarios.LimiteIsencao;

            if (lucro > 0)
            {
                if (carteira.PrejuizoAcumulado >= lucro)
                {
                    carteira.PrejuizoAcumulado = FormatadorDecimal.Round2(
                        carteira.PrejuizoAcumulado - lucro
                    );
                }
                else
                {
                    var lucroTributavel = lucro - carteira.PrejuizoAcumulado;
                    carteira.PrejuizoAcumulado = 0m;

                    if (!isento)
                    {
                        imposto = FormatadorDecimal.Round2(
                            lucroTributavel * ParametrosTributarios.PercentualImposto
                        );
                    }
                }
            }
            else if (lucro < 0)
            {
                carteira.PrejuizoAcumulado = FormatadorDecimal.Round2(
                    carteira.PrejuizoAcumulado + (-lucro)
                );
            }

            carteira.Quantidade -= quantidade;
        }
    }
}
