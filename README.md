# UsuariosApp

O UsuariosApp é um sistema de controle de usuários desenvolvido em ASP.NET API com o uso do Entity Framework, JWT, XUnit e padrões DDD (Domain Driven Design) e TDD (Test Driven Development).

## Pré-requisitos

Para executar este projeto, você precisará do seguinte:

- [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/vs/) com .NET 8 instalado.
- Um banco de dados SQL Server. Você precisará criar um banco de dados local e atualizar a string de conexão no arquivo `DataContext.cs`.

## Configuração do Banco de Dados

Após configurar o banco de dados local e atualizar a string de conexão no arquivo DataContext.cs, execute o comando 'Update-Database' no Console do Gerenciador de Pacotes do Visual Studio para aplicar as migrações e criar as tabelas necessárias.
