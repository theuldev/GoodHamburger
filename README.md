# Good Hamburger

Esta é a solução para a lanchonete "Good Hamburger", um sistema projetado para registrar os pedidos, calcular subtotais e descontos automaticamente com base em regras de negócio. O projeto conta com uma **API REST** (C# ASP.NET Core) e um **Frontend em Blazor**.

## Regras de Negócio e Descontos

O cardápio atual consiste em:

**Sanduíches:**
* X Burger — R$ 5,00
* X Egg — R$ 4,50
* X Bacon — R$ 7,00

**Acompanhamentos:**
* Batata frita — R$ 2,00
* Refrigerante — R$ 2,50

**Regras de Desconto:**
* Sanduíche + batata + refrigerante → 20% de desconto
* Sanduíche + refrigerante → 15% de desconto
* Sanduíche + batata → 10% de desconto
* *Nota:* Cada pedido pode conter apenas **um item** de cada categoria (1 sanduíche, 1 acompanhamento, 1 bebida). Itens duplicados por categoria resultarão em erro de validação claro.

---

## 🚀 Como Executar o Projeto

O projeto foi inteiramente containerizado utilizando o **Docker Compose** para facilitar a execução, sem a necessidade de instalar SDKs e configurar o banco manualmente. A stack sobe o Banco de Dados (PostgreSQL), o pgAdmin, a API e o Frontend Web (Blazor).

### Pré-requisitos
* **Docker** e **Docker Compose** instalados.
* *(Opcional)* .NET 10.0 SDK instalado caso deseje rodar o código localmente fora dos containers.

### Executando com Docker (Recomendado)
A partir da raiz do repositório, rode o seguinte comando:
```bash
docker-compose up -d --build
```
Isso fará o build das imagens (API e Web) e iniciará todos os serviços. As migrações do banco de dados são executadas automaticamente pela API durante o startup.

Após a inicialização, os serviços estarão disponíveis em:
* **Frontend (Blazor):** [http://localhost:8080](http://localhost:8080)
* **API:** [http://localhost:5000](http://localhost:5000)
* **pgAdmin:** [http://localhost:5050](http://localhost:5050)
  * Email: `admin@admin.com` / Senha: `admin`
  * *(Para conectar ao banco via pgAdmin, adicione um novo servidor: Host `db`, Usuário `postgres`, Senha `postgres`)*
* **PostgreSQL:** disponível externamente na porta `5432`

### Executando Manualmente (.NET CLI)
Caso prefira rodar os projetos usando o SDK:
1. Suba apenas a infraestrutura: `docker-compose up -d db pgadmin`
2. Na raiz, rode as migrações (se não aplicadas ainda): `dotnet ef database update --project GoodHamburger.Infra --startup-project GoodHamburger.API`
3. Execute a API: `dotnet run --project GoodHamburger.API`
4. Execute o Web: `dotnet run --project GoodHamburger.Web`

---

## 🧪 Testes Automatizados

Para garantir a integridade das regras de negócio, cálculo de descontos e exceções, o projeto possui testes automatizados (unitários e de integração) escritos com **xUnit** e **Moq**.

Para executar:
```bash
dotnet test
```

---

## 🏗 Arquitetura e Decisões Técnicas

A arquitetura do backend foi baseada nos princípios do **Domain-Driven Design (DDD)** estruturada em 4 camadas principais:

1. **GoodHamburger.Domain:** Contém as entidades principais do sistema, as regras de negócio puras (ex.: cálculo de desconto e limite de itens) e as Exceptions de domínio. É a camada mais isolada, sem dependências externas.
2. **GoodHamburger.Application:** Onde orquestramos os casos de uso da aplicação (Services), fazemos o mapeamento dos dados (ViewModels/InputModels/DTOs) e coordenamos as invocações de persistência.
3. **GoodHamburger.Infra:** Implementa as interfaces do repositório utilizando o **Entity Framework Core** e gerencia a comunicação real com o PostgreSQL.
4. **GoodHamburger.API:** Controladores REST (Endpoints), injeção de dependência e Middlewares (como o tratamento global de exceções, garantindo respostas padronizadas de erro).

**Outras decisões importantes:**
- **PostgreSQL:** Escolhido pela robustez e aderência com aplicações corporativas, suportado pelo EF Core.
- **FluentValidation:** Utilizado para retirar regras de validação (ex: campos obrigatórios) do controlador e garantir validações de entrada claras e concisas.
- **Blazor (Frontend):** Atendendo ao diferencial do desafio, foi criado um cliente SPA para consumir os endpoints da API.
- **Containerização Total:** Todo o projeto em Docker/Docker Compose para reduzir atritos de ambiente ("na minha máquina funciona").