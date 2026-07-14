# TattooStudio

Sistema web MVC para gestao simples de um estudio de tatuagem. O projeto organiza clientes, sessoes realizadas, formas de pagamento e resumo financeiro por periodo.

## Tecnologias

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Razor Views
- Bootstrap

## Funcionalidades

- Cadastro, edicao, listagem e exclusao de clientes.
- Cadastro, edicao, listagem e exclusao de sessoes de tattoo.
- Vinculo entre cliente e sessao.
- Historico de sessoes dentro da pagina de detalhes do cliente.
- Criacao de sessao a partir do historico com cliente ja selecionado.
- Retorno automatico ao historico do cliente apos salvar uma sessao criada por esse fluxo.
- Navegacao direta da lista de sessoes para o historico do cliente.
- Validacao de campos obrigatorios e CPF duplicado.
- Validacao de cliente valido, valor pago e data da sessao.
- Resumo financeiro por periodo com total de sessoes e valor arrecadado.

## Organizacao tecnica

- Controllers concentram o fluxo MVC e delegam regras de persistencia aos services.
- Services centralizam consultas e operacoes com Entity Framework Core.
- ViewModels separam os dados usados por formularios das entidades persistidas no banco.
- Razor Views entregam as telas com validacao visual e navegacao contextual.

## Como executar

1. Configure a connection string `WebAppTattooContext` em `appsettings.json`.
2. Restaure os pacotes NuGet.
3. Aplique as migrations do Entity Framework.
4. Execute o projeto pelo Visual Studio ou com `dotnet run`.

## Objetivo do projeto

Este projeto foi desenvolvido como estudo pratico de ASP.NET Core MVC, Entity Framework Core e modelagem de um fluxo de negocio real. A ideia e demonstrar dominio de CRUD, relacionamento entre entidades, validacoes, consultas com filtros e organizacao basica em camadas.

## Proximos passos

- Adicionar testes automatizados para clientes, sessoes e resumo financeiro.
- Evoluir os nomes de dominio para refletir melhor o conceito de sessao de atendimento.
- Adicionar screenshots das principais telas para melhorar a apresentacao no GitHub.
