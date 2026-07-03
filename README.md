# CRUDDOTNET

API CRUD de usuários em C# (.NET 8, ASP.NET Core + Entity Framework Core) com PostgreSQL, empacotada com Docker.

## Endpoints

| Método | Rota              | Descrição                    |
| ------ | ----------------- | ---------------------------- |
| GET    | `/api/users`      | Lista todos os usuários      |
| GET    | `/api/users/{id}` | Busca um usuário pelo id     |
| POST   | `/api/users`      | Cria um usuário              |
| PUT    | `/api/users/{id}` | Atualiza um usuário          |
| DELETE | `/api/users/{id}` | Remove um usuário            |

Documentação interativa (Swagger): `http://localhost:8080/swagger`.

Exemplo de corpo para POST/PUT:

```json
{
  "nome": "Fulano de Tal",
  "cpf": "00000000000",
  "email": "fulano@example.com",
  "dataNasc": "1990-01-01T00:00:00Z"
}
```

## Como rodar

### Com Docker (recomendado)

```bash
docker compose up --build
```

A API sobe em `http://localhost:8080` e o PostgreSQL em `localhost:5432`. As migrations são aplicadas automaticamente na inicialização.

### Localmente (sem Docker para a API)

Requer o SDK do .NET 8 e um PostgreSQL rodando (pode ser o do compose: `docker compose up db`).

```bash
dotnet run --project src/CRUDDOTNET
```

A connection string de desenvolvimento fica em `src/CRUDDOTNET/appsettings.Development.json`; em produção, defina a variável de ambiente `ConnectionStrings__DefaultConnection`.

Também há requisições prontas em `src/CRUDDOTNET/CRUDDOTNET.http`.

## Testes

```bash
dotnet test tests/CRUDDOTNET.Tests/CRUDDOTNET.Tests.csproj
```

Os testes cobrem o CRUD do `UsersController` usando o provider in-memory do EF Core e rodam no CI (GitHub Actions) a cada push/PR.
