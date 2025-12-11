# Blog API

API RESTful em .NET 8 para gerenciar posts de blog e comentários. Projeto seguindo Clean Architecture e DDD.

## Estrutura do Projeto

```
BlogApi.Domain          - Entidades e interfaces de repositórios
BlogApi.Application     - Casos de uso e DTOs
BlogApi.Infrastructure - Implementação de repositórios e DbContext
BlogApi.API            - Controllers e configurações
```

## Requisitos

- .NET 8 SDK
- SQLite (vem embutido, não precisa instalar nada)

## Como Rodar

```bash
# Restaurar dependências
dotnet restore

# Compilar
dotnet build

# Executar
cd BlogApi.API
dotnet run
```

A API vai rodar em `http://localhost:5110` e o Swagger fica na raiz (`http://localhost:5110`).

## Endpoints

### Posts

**GET /api/posts**
- Retorna lista de posts com título e quantidade de comentários

**POST /api/posts**
- Cria um novo post
- Body: `{ "titulo": "string", "conteudo": "string" }`

**GET /api/posts/{id}**
- Retorna post completo com todos os comentários

### Comentários

**POST /api/posts/{postId}/comments**
- Adiciona comentário em um post
- Body: `{ "conteudo": "string", "autor": "string" }`

## Testando

A forma mais fácil é pelo Swagger. Depois de rodar a aplicação, acesse `http://localhost:5110`.

**Ordem recomendada para testar:**
1. Criar um post (POST /api/posts) - copie o `id` retornado
2. Listar posts (GET /api/posts)
3. Ver post por ID (GET /api/posts/{id})
4. Adicionar comentário (POST /api/posts/{postId}/comments) - use o `id` do passo 1

## Banco de Dados

SQLite. O arquivo `blog.db` é criado automaticamente na pasta `BlogApi.API` na primeira execução.

## Stack

- .NET 8
- Entity Framework Core
- SQLite
- Swagger/OpenAPI

## Próximos Passos

Se tivesse mais tempo, priorizaria:

1. **Migrations** - Trocar o `EnsureCreated` por migrations do EF Core. É mais seguro e permite versionar mudanças no banco.

2. **Testes** - Começaria pelos casos de uso, depois controllers. Sem testes fica difícil refatorar com confiança.

3. **Paginação** - O endpoint de listar posts vai quebrar quando tiver muitos registros. Adicionar `skip` e `take` seria o mínimo.

4. **CRUD completo** - Faltam endpoints de atualizar e deletar posts/comentários. Básico mas necessário.

5. **Autenticação** - Se for para produção, JWT seria essencial. Mas depende do contexto do projeto.

6. **Health checks** - Rápido de implementar e ajuda no monitoramento.

7. **Docker** - Facilita deploy e ambiente de desenvolvimento.

A ordem depende do que for mais crítico no momento, mas migrations e testes seriam minha prioridade.
