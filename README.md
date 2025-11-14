# TakeHomeExercise

Este projeto é uma aplicação console em C# que calcula o imposto (“taxa”) sobre operações de compra e venda de ações.

## 🔧 Como rodar o projeto

1. Abra o projeto no Visual Studio.
2. Execute com Ctrl + F5.
3. Digite uma linha com JSON no formato abaixo:
[{"operation":"buy","unit-cost":10,"quantity":10000},{"operation":"sell","unit-cost":20,"quantity":5000}]

4. O programa retorna um JSON com a taxa calculada:
[{"taxa":10000}]



## 📂 Estrutura

- **Entidades/** → classes de dados (Operacao, Carteira, TaxResultado)
- **Services/** → regras de negócio
- **Util/** → constantes e funções auxiliares
- **Program.cs** → ponto de entrada da aplicação

## 📘 O que o programa faz

- Calcula custo médio
- Calcula lucro de vendas
- Controla prejuízo acumulado
- Aplica isenção até 20.000
- Calcula imposto de 20% quando necessário