# API GESTAO BANCARIA

Desafio técnico da criação de uma API Web do [ASP.NET](http://ASP.NET) CORE

Tecnologias utilizadas: [ASP.NET](http://ASP.NET) CORE, ENTITY FRAMEWORK, POSTGRESQL, SWAGGER.

Funcionalidades: Cadastro de Usuario, Login, Depositar valor, Consultar extrato, Transferir valor entre contas.

## INSTRUÇÕES PARA CONEXÃO COM BANCO DE DADOS:

—————————————————————————————————————

1 - **Instalação dos Pacotes Necessários:**

Pacotes necessários:

Microsoft.EntityFrameworkCore  / versão utilizada na época da implementação (7.0.11).
Microsoft.EntityFrameworkCore.Tools  / versão utilizada na época da implementação  (7.0.11).
Npgsql.EntityFrameworkCore.PostgreSQL  / versão utilizada na época da implementação (7.0.4).

2 - **Configurar a String de Conexão**:

Realize a configuração de sua string de conexão com o banco de dados presente no arquivo ‘Program.cs’, há um comentário sinalizando o local correto da inserção da string de conexão.

3 - **Executar as Migrações**: 

Após configurar a string de conexão, abra a interface de linha de comando do gerenciador de pacotes NuGet e execute as migrações.

Comando para executar as migrações:

```csharp
Update-Database
```

Este comando aplicará as migrações pendentes ao banco de dados, criando ou atualizando-o de acordo com o estado definido pelas migrações.

**Após isso execute o programa, ele deverá funcionar como o esperado.**

## PACOTES UTILIZADOS E SUAS RESPECTIVAS VERSÕES:

——————————————————————————————————————

**REFERENTES A AUTENTICAÇÃO POR JWT:**

Microsoft.AspNetCore.Authentication.JwtBearer (7.0.11)

System.IdentityModel.Tokens.Jwt (7.0.0)

Microsoft.IdentityModel.Tokens (7.0.0)

**REFERENTES A BANCO DE DADOS:**

Microsoft.EntityFrameworkCore (7.0.11)
Microsoft.EntityFrameworkCore.Tools (7.0.11)
Npgsql.EntityFrameworkCore.PostgreSQL (7.0.4)

**REFERENTES A DOCUMENTAÇÃO DA API (Swagger/OpenAPI):**

Microsoft.AspNetCore.OpenApi (7.0.9)
Swashbuckle.AspNetCore (6.5.0)
