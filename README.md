# ManagerProgram — Sistema de Gestão Escolar

Aplicação web para gestão escolar (cadastro de alunos, cidades, séries, turmas e matrículas),
construída em ASP.NET Core MVC sobre banco Firebird 5, com arquitetura em camadas.

## Tecnologias

- .NET 10 / ASP.NET Core MVC
- Firebird 5 (`FirebirdSql.Data.FirebirdClient`)
- Acesso a dados em ADO.NET puro, via DAOs
- Bootstrap 5 + jQuery Validation nas views

## Arquitetura

O projeto é dividido em quatro camadas, cada uma em seu próprio `.csproj`:

| Camada | Responsabilidade |
|---|---|
| `Domain` | Entidades, enums e interfaces de repositório. Não depende de ninguém. |
| `Application` | Serviços de aplicação — orquestram as regras de negócio. |
| `Infrastructure` | Implementação dos repositórios (DAOs Firebird) e mappers. |
| `Web` | Camada de apresentação: controllers, views Razor e configuração. |

A dependência aponta sempre para dentro: `Web` → `Application`/`Infrastructure` → `Domain`.

```
ManagerProgram/
├── Domain/            Entidades, enums, interfaces
├── Application/       Serviços de aplicação
├── Infrastructure/    DAOs Firebird e mappers
├── Web/               Controllers, Views, wwwroot
└── Database/          schema.sql e documentação do banco
```

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- [Firebird 5](https://firebirdsql.org/en/firebird-5-0/) rodando como serviço
- Um IDE a gosto (Rider, Visual Studio ou VS Code)

## Como rodar

**1. Clonar o repositório**

```bash
git clone https://github.com/SEU-USUARIO/ManagerProgram.git
cd ManagerProgram
```

**2. Criar o banco de dados**

O arquivo `.fdb` não é versionado — ele é gerado a partir do script de schema.
A partir da pasta `Database/`:

```bash
isql -i schema.sql
```

Isso cria o `GestaoEscolar.fdb` com todas as tabelas, constraints, o trigger de
regra de negócio e uma carga de exemplo. Detalhes em [`Database/README-Database.md`](Database/README-Database.md).

**3. Configurar as credenciais**

As credenciais ficam fora do controle de versão. Copie o arquivo de exemplo e
preencha com os dados do seu Firebird local:

```bash
cp Web/appsettings.Development.example.json Web/appsettings.Development.json
```

```json
"Firebird": {
  "User": "SYSDBA",
  "Password": "sua_senha",
  "DataSource": "localhost",
  "Port": 3055
}
```

**4. Executar**

```bash
dotnet run --project Web
```

A aplicação sobe em `http://localhost:5211` (ou `https://localhost:7213`).

## Autor

Desenvolvido como projeto para praticas em desenvolvimento.
