# FCG Catalog API 🎮

Este repositório contém o **Microsserviço de Catálogo**, um dos componentes principais da plataforma FIAP Cloud Games (FCG), construído em uma arquitetura orientada a eventos.

A principal responsabilidade deste serviço é o gerenciamento do acervo de jogos da plataforma (CRUD) e o controle das bibliotecas digitais dos usuários. Além disso, o Catálogo é o responsável por iniciar todo o fluxo de compra assíncrono e concluí-lo após a confirmação financeira.

## 🎯 Responsabilidades e Fluxo de Eventos

O Catalog API atua como o motor do fluxo de negócios, operando tanto como **Publicador** quanto como **Consumidor** no barramento de mensageria (RabbitMQ):

1. **Gestão de Acervo:** 
   * Provê o endpoint necessário para o cadastro dos jogos.

2. **Início do Fluxo de Compra (Publisher):**
   * **Gatilho:** Recebe uma requisição HTTP do usuário para comprar um jogo.
   * **Ação:** Valida a existência do jogo e publica o evento `OrderPlacedEvent` (contendo `UserId`, `GameId` e `Price`) na mensageria para ser processado pelo serviço de Pagamentos.

3. **Efetivação da Compra (Consumer):**
   * **Consome:** `PaymentProcessedEvent` (publicado pelo `PaymentsAPI`).
   * **Ação:** Verifica o status do processamento. Se o pagamento for `Approved` (Aprovado), o serviço vincula permanentemente o jogo à biblioteca pessoal do usuário em seu banco de dados.

## 🛠️ Tecnologias Utilizadas

* **Framework:** .NET 8 (ASP.NET Core Web API)
* **Banco de Dados:** PostgreSQL (Padrão *Database per Service*)
* **ORM:** Entity Framework Core
* **Mensageria:** RabbitMQ
* **Integração de Mensageria:** MassTransit
* **Containerização:** Docker (Multi-stage build)
* **Orquestração:** Kubernetes (K8s)

## 📁 Estrutura do Projeto

* `/src`: Código-fonte da aplicação C# (.NET 8).
* `/Dockerfile`: Arquivo de configuração para a criação da imagem Docker otimizada para produção.
* `/k8s`: Diretório contendo os manifestos do Kubernetes:
  * `deployment.yaml` e `service.yaml`: Manifestos de execução e exposição da API.
  * `database.yaml`: Provisionamento do banco de dados PostgreSQL exclusivo deste serviço.
  * `secret.yaml`: Gerenciamento de credenciais e string de conexão (Base64).

## ⚙️ Variáveis de Ambiente e Configurações

Para garantir a escalabilidade e segurança no cluster Kubernetes, a API depende de variáveis injetadas via `ConfigMaps` e `Secrets`:

| Variável / Configuração | Descrição | Exemplo |
| :--- | :--- | :--- |
| `ConnectionStrings:DefaultConnection` | String de conexão com o banco de dados | `Host=catalog-db;Database=catalog_db;Username=admin;Password=...` |
| `RabbitMQ:Host` | Endereço do broker de mensageria | `rabbitmq` (Nome do Service no K8s) |
| `RabbitMQ:Username` | Usuário de autenticação da fila | `guest` |
| `RabbitMQ:Password` | Senha de autenticação da fila | `guest` |

## 🚀 Como Executar

A orquestração de todos os serviços simultaneamente é gerenciada pelo repositório **[FCG-Orchestration](https://github.com/Gabriel-Bacelar-Valentim/FCG-Orchestration)**.

### Via Docker Compose (Ambiente de Desenvolvimento)
Para subir o ambiente completo localmente, acesse o repositório de Orquestração e execute:
```bash
docker-compose up --build
