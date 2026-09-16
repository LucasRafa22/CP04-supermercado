# 📌 CP4 — Health Checks, Observabilidade e Testes com xUnit

## 👥 Integrantes

* Lucas Rafael Solimene — RM 565194
* Samyr Couto Oliveira — RM 565562

---

## 🎯 Domínio da Aplicação

Sistema de **Supermercado**, contendo:

* Clientes
* Produtos
* Categorias
* Vendas
* Itens de Venda

A API permite operações CRUD com persistência em banco de dados via **Entity Framework Core**.

---

## 🗄️ Banco de Dados

* SGBD: **Oracle**
* Acesso via **EF Core**
* Configurado no `ApplicationDbContext`

---

## 🚀 Como Executar a API

Copie o projeto

```bash
git clone https://github.com/LucasRafa22/CP04-supermercado.git
```

Coloque as credencias do banco em appsettings.Development.json

```bash
"RecommendaContextOracle" : "Data Source=oracle.fiap.com.br:1521/orcl;User ID=<USUARIO>;Password=<SENHA>;"
```

Depois execute os seguintes comandos

```bash
cd Supermercado.API
dotnet restore
dotnet build
dotnet run
```

### 🔗 URLs importantes

* Swagger:
  👉 `https://localhost:5084/swagger`

* Health Check:
  👉 `https://localhost:5084/health`

---

## 🩺 Health Check

Endpoint único:

```
GET /health
```

### ✔ Checks implementados:

* ✔ **self** → verifica se a API está rodando
* ✔ **database** → verifica conexão com banco Oracle

### 📊 Exemplo de resposta:

```json
{
  "status": "Healthy",
  "duration": 10.23,
  "checks": [
    {
      "name": "database",
      "status": "Healthy",
      "duration": 8.12,
      "error": null
    },
    {
      "name": "self",
      "status": "Healthy",
      "duration": 0.45,
      "error": null
    }
  ]
}
```

### 📌 Status HTTP:

* `200` → Healthy / Degraded
* `503` → Unhealthy

---

## 📊 Observabilidade (Logs)

A aplicação utiliza **ILogger** com logs estruturados.

### ✔ Implementado:

* Logs em operações de escrita (POST/PUT/DELETE)
* Logs de erro no `GlobalExceptionHandler`
* Correlação com:

```
traceId = HttpContext.TraceIdentifier
```

### 📌 Exemplo de log:

```
[INFO] Criando produto {Nome} | traceId: abc123
[ERROR] Erro inesperado | traceId: abc123
```

---

## 🧪 Testes Automatizados

### ✔ Projetos de teste:

* `Supermercado.Domain.Tests`
* `Supermercado.Application.Tests`

---

### 🧱 Domain Tests (sem mock)

* ✔ Testes de regra de negócio real
* ✔ Uso de:

  * `[Fact]`
  * `[Theory]`
* ✔ Validação de exceções

---

### ⚙️ Application Tests (com mock)

* ✔ Uso de **Moq**
* ✔ Mock de `IRepository<T>`
* ✔ Cenários testados:

  * Falha → NÃO persiste (`Times.Never`)
  * Sucesso → persiste (`Times.Once`)

---

### ▶️ Executar testes

```bash
dotnet test
```

✔ Todos os testes devem passar

---

## ⚠️ Tratamento de Erros

Implementado com:

* `GlobalExceptionHandler`
* `ProblemDetails` (RFC 7807)

### 📌 Mapeamento:

| Exceção              | HTTP |
| -------------------- | ---- |
| ArgumentException    | 400  |
| KeyNotFoundException | 404  |
| Exception            | 500  |

---

## 📁 Evidências (/docs)

A pasta `/docs` contém:

* ✔ Print do `/health` (Healthy)
* ✔ Print do `/health` (Unhealthy)
* ✔ Logs com `traceId`
* ✔ Saída do `dotnet test`

---

## 🧱 Arquitetura

Projeto segue **Clean Architecture**:

* **API** → Controllers + configuração
* **Application** → DTOs + interfaces
* **Domain** → entidades + regras de negócio
* **Infrastructure** → EF Core + repositórios

---

## 🔄 Repositório Genérico

Implementado:

```
IRepository<T>
Repository<T>
```

### ✔ Operações:

* GetAll
* GetById
* Add
* Delete

Utilizado nos controllers e testes.
