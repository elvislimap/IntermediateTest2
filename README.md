# IntermediateTest2

Solução realizada:
------------------

- Cadastro de funcionários com valor do salário para realizar o cálculo de 8% do fundo compartilhado
- Cadastro de ajuste de inflação para realizar os cálculos de reajustes mensalmente de acordo com a taxa do mês corrente
- Cadastro do fundo compartilhado
- Saque do fundo compartilhado 1 vez quando aniversário do funcionário. Obtém o saldo total, identifica o valor fixo a receber somando com o valor recuperado a partir do percentual de limite do saldo total de acordo com a tabela no fim do documento.

OBS: Ao cadastrar um fundo compartilhado, informar apenas o EmployeeId e ContributionDate. O valor do fundo é calculado de acordo com o salário do funcionário, caso infome o valor o serviço retornará BadRequest.

Recursos utilizados no projeto:
-------------------------------

- .NET Core 2.2
- EF Core 2.2
- TDD - Escrita de testes unitários antes do código fonte dos serviços utilizando XUnit
- DDD - Modelagem do software
- SOLID - Boas práticas de programação orientada a objetos
- Swagger - Documentação da API

Regras para saque:
-------------------------------

| Balance                     | % Limit | Fixed money |
|-----------------------------|---------|-------------|
| Up until $500.00            | 50%     | $0.00       |
| From $500.01 to $1000.00    | 40%     | $50.00      |
| From $1000.01 to $5000.00   | 30%     | $150.00     |
| From $5000.01 to $10000.00  | 20%     | $650.00     |
| From $10000.01 to $15000.00 | 15%     | $1150.00    |
| From $15000.01 to $20000.00 | 10%     | $1900.00    |
| From $20000.01              | 5%      | $2900.00    |
