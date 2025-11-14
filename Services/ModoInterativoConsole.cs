using System;
using System.Globalization;
using System.Text;
using TakeHomeExercise.Entidades;

namespace TakeHomeExercise.Services
{
    public static class ModoInterativoConsole
    {
        public static void Executar()
        {
            Console.OutputEncoding = Encoding.UTF8;
            var cultura = new CultureInfo("pt-BR");
            var carteira = new Carteira();

            DesenharCabecalho();

            while (true)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("comando> ");
                Console.ResetColor();

                var linha = Console.ReadLine();
                if (linha == null)
                    continue;

                linha = linha.Trim();
                if (linha.Length == 0)
                    continue;

                var partes = linha.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var comando = partes[0].ToLowerInvariant();

                if (comando == "sair" || comando == "exit" || comando == "q")
                    break;

                if (comando == "ajuda" || comando == "help" || comando == "h")
                {
                    MostrarAjuda();
                    continue;
                }

                if (comando == "limpar" || comando == "cls")
                {
                    Console.Clear();
                    DesenharCabecalho();
                    continue;
                }

                if (comando == "saldo")
                {
                    MostrarSaldo(carteira, cultura);
                    continue;
                }

                if (comando == "comprar" || comando == "buy")
                {
                    if (!TentarLerQuantidadePreco(partes, out var quantidade, out var preco))
                    {
                        MostrarErro("Uso: comprar <quantidade> <precoUnitario>");
                        continue;
                    }

                    CarteiraServico.RegistrarCompra(carteira, preco, quantidade);

                    Console.WriteLine();
                    DesenharLinha();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("COMPRA EXECUTADA");
                    Console.ResetColor();
                    Console.WriteLine($"Quantidade comprada : {quantidade}");
                    Console.WriteLine($"Preço unitário      : {preco.ToString("C2", cultura)}");
                    Console.WriteLine($"Quantidade total    : {carteira.Quantidade}");
                    Console.WriteLine($"Custo médio atual   : {carteira.CustoMedio.ToString("C2", cultura)}");
                    DesenharLinha();
                    continue;
                }

                if (comando == "vender" || comando == "sell")
                {
                    if (!TentarLerQuantidadePreco(partes, out var quantidade, out var preco))
                    {
                        MostrarErro("Uso: vender <quantidade> <precoUnitario>");
                        continue;
                    }

                    if (quantidade > carteira.Quantidade)
                    {
                        MostrarErro($"Quantidade insuficiente em carteira. Saldo atual: {carteira.Quantidade}");
                        continue;
                    }

                    CarteiraServico.RegistrarVenda(carteira, preco, quantidade, out var lucro, out var imposto);

                    Console.WriteLine();
                    DesenharLinha();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("VENDA EXECUTADA");
                    Console.ResetColor();
                    Console.WriteLine($"Quantidade vendida  : {quantidade}");
                    Console.WriteLine($"Preço unitário      : {preco.ToString("C2", cultura)}");

                    Console.Write("Lucro/Prejuízo      : ");
                    if (lucro > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (lucro < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    Console.WriteLine(lucro.ToString("C2", cultura));
                    Console.ResetColor();

                    Console.Write("Imposto             : ");
                    if (imposto > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    Console.WriteLine(imposto.ToString("C2", cultura));
                    Console.ResetColor();

                    Console.WriteLine($"Prejuízo acumulado  : {carteira.PrejuizoAcumulado.ToString("C2", cultura)}");
                    Console.WriteLine($"Quantidade restante : {carteira.Quantidade}");
                    DesenharLinha();
                    continue;
                }

                MostrarErro("Comando inválido. Use: comprar, vender, saldo, ajuda, limpar, sair");
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Encerrando modo interativo...");
            Console.ResetColor();
        }

        private static bool TentarLerQuantidadePreco(string[] partes, out int quantidade, out decimal preco)
        {
            quantidade = 0;
            preco = 0m;

            if (partes.Length < 3)
                return false;

            if (!int.TryParse(partes[1], out quantidade))
                return false;

            var textoPreco = partes[2].Replace(',', '.');
            if (!decimal.TryParse(textoPreco, NumberStyles.Number, CultureInfo.InvariantCulture, out preco))
                return false;

            return true;
        }

        private static void MostrarSaldo(Carteira carteira, CultureInfo cultura)
        {
            Console.WriteLine();
            DesenharLinha();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("SALDO DA CARTEIRA");
            Console.ResetColor();
            Console.WriteLine($"Quantidade          : {carteira.Quantidade}");
            Console.WriteLine($"Custo médio         : {carteira.CustoMedio.ToString("C2", cultura)}");
            Console.WriteLine($"Prejuízo acumulado  : {carteira.PrejuizoAcumulado.ToString("C2", cultura)}");
            DesenharLinha();
        }

        private static void MostrarAjuda()
        {
            Console.WriteLine();
            DesenharLinha();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("COMANDOS DISPONÍVEIS");
            Console.ResetColor();
            Console.WriteLine("comprar <qtd> <preco>  - Registra uma compra");
            Console.WriteLine("vender  <qtd> <preco>  - Registra uma venda e calcula imposto");
            Console.WriteLine("saldo                  - Mostra situação atual da carteira");
            Console.WriteLine("limpar                 - Limpa a tela");
            Console.WriteLine("ajuda                  - Mostra esta ajuda");
            Console.WriteLine("sair                   - Encerra o programa");
            DesenharLinha();
        }

        private static void MostrarErro(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }

        private static void DesenharCabecalho()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================================");
            Console.WriteLine("            TAKE HOME BROKER - CLI             ");
            Console.WriteLine("===============================================");
            Console.ResetColor();
            Console.WriteLine("Digite 'ajuda' para ver os comandos disponíveis.");
            Console.WriteLine();
        }

        private static void DesenharLinha()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("-----------------------------------------------");
            Console.ResetColor();
        }
    }
}
