# Banco de dados — Sistema de Gestão Escolar (Firebird 5)

## Arquivos

- `GestaoEscolar.fdb` — banco de dados Firebird 5 já criado, com as 6 tabelas do domínio, constraints, o trigger de regra de negócio e uma carga de exemplo (3 cidades, 2 alunos, 2 montagens série-turma, 1 matrícula).
- `schema.sql` — o script completo que gerou o `.fdb` acima (DDL + seed). Use-o para recriar o banco do zero a qualquer momento.

Criado e validado localmente com o engine real do **Firebird 5.0.1** (`isql`/`gbak` da distribuição oficial) — não é um arquivo montado à mão, é um banco Firebird de verdade, pronto para abrir.

## Credenciais

```
Usuário: SYSDBA
Senha:   masterkey
```

São as credenciais padrão do Firebird — troque a senha do SYSDBA antes de usar em qualquer ambiente que não seja a sua máquina de estudo (`gsec -user sysdba -password masterkey` ou o utilitário `chpasswd` do seu Firebird Server).

## Como usar

1. Copie `GestaoEscolar.fdb` para a pasta de dados do seu Firebird Server (ex.: `C:\Firebird5\data\`), ou aponte o caminho diretamente no connection string — Firebird acessa o arquivo onde ele estiver, não precisa estar em uma pasta específica.
2. Confirme que o serviço "Firebird Server - DefaultInstance" está rodando (Firebird 5 para Windows instala como serviço).
3. Configure a connection string no `Web/appsettings.json` (ainda não existe uma seção `ConnectionStrings` lá — adicione algo como):

```json
"ConnectionStrings": {
  "Firebird": "User=SYSDBA;Password=masterkey;Database=C:\\Caminho\\Para\\GestaoEscolar.fdb;DataSource=localhost;Port=3050;Dialect=3;Charset=UTF8;"
}
```

Isso é o que a camada de Infraestrutura (`FbConnection`) vai ler para abrir a conexão — ver a seção "Apresentação — ASP.NET Core MVC" do documento de requisitos, no trecho de `Program.cs`.

## Recriar o banco do zero

```
isql -i schema.sql
```

(rode a partir da pasta onde quer que o `.fdb` seja criado — o script tem `CREATE DATABASE 'GestaoEscolar.fdb'` com caminho relativo).

## O que já foi testado

- Todas as 6 tabelas (`CIDADE`, `ALUNO`, `SERIE`, `TURMA`, `SERIE_TURMA`, `MATRICULA`) criadas com as constraints `CHECK`, `UNIQUE` e `FOREIGN KEY` do documento de requisitos.
- O trigger `TRG_MATRICULA_UNICA_ANO` (regra RN06 — uma matrícula ativa por aluno por ano letivo) foi testado tentando inserir uma segunda matrícula ativa para o mesmo aluno no mesmo ano: o Firebird recusou com a exceção `EX_MATRICULA_DUPLICADA_NO_ANO`, como esperado.
- A consulta de conferência (aluno + matrícula + ano letivo) retornou os dados de exemplo corretamente.

## Se preferir o banco vazio

Abra `schema.sql`, apague o bloco final a partir do comentário `/* Carga inicial (seed) ... */` e rode o script de novo — todo o resto (tabelas, constraints, trigger) continua igual.
