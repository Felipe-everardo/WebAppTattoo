# TattooStudio

Sistema web MVC para gestão simples de um estúdio de tatuagem. O projeto organiza clientes, sessões realizadas, formas de pagamento e resumo financeiro por período.

## Tecnologias

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Razor Views
- Bootstrap

## Funcionalidades

- Cadastro, edição, listagem e exclusão de clientes.
- Cadastro, edição, listagem e exclusão de sessões de tattoo.
- Vínculo entre cliente e sessão.
- Histórico de sessões dentro da página de detalhes do cliente.
- Validação de campos obrigatórios e CPF duplicado.
- Resumo financeiro por período com total de sessões e valor arrecadado.

## Como executar

1. Configure a connection string `WebAppTattooContext` em `appsettings.json`.
2. Restaure os pacotes NuGet.
3. Aplique as migrations do Entity Framework.
4. Execute o projeto pelo Visual Studio ou com `dotnet run`.

## Objetivo do projeto

Este projeto foi desenvolvido como estudo prático de ASP.NET Core MVC, Entity Framework Core e modelagem de um fluxo de negócio real. A ideia é demonstrar domínio de CRUD, relacionamento entre entidades, validações, consultas com filtros e organização básica em camadas.
