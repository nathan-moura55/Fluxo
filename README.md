# 📦 Fluxo - Gestão de Estoque Full-Stack

O **Fluxo** é uma solução completa para controle de inventário industrial e comercial, desenvolvida com uma arquitetura moderna dividida entre um backend robusto em **.NET** e um frontend dinâmico em **React**. O foco principal do projeto é a integridade dos dados e a experiência do usuário na gestão de movimentações.

---

### 🛠 Tecnologias e Ferramentas

<div align="left">
  <img src="https://img.shields.io/badge/react-%2320232a.svg?style=for-the-badge&logo=react&logoColor=%2361DAFB" />
  <img src="https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white" />
  <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=.net&logoColor=white" />
  <img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" />
</div>

---

### 🚀 Diferenciais Técnicos

Neste projeto, foram aplicadas boas práticas de desenvolvimento para resolver desafios reais de sistemas SaaS:

* **Sincronização em Tempo Real:** Utilização de `async/await` no React para garantir que a interface reflita o estado do banco de dados imediatamente após cada operação (Sem "atropelar" o processamento do backend).
* **Trilha de Auditoria (Logs):** Implementação automática de logs de movimentação. Cada saída de estoque gera um registro histórico com timestamp UTC, garantindo rastreabilidade total.
* **Regras de Negócio no Backend:**
    * Impedimento de estoque negativo diretamente na camada de serviço.
    * Bloqueio de exclusão para produtos com saldo residual (Segurança Contábil).
    * Tratamento de exceções customizado para retornar mensagens claras ao usuário.
* **Interface Inteligente:** Alertas visuais automáticos (linhas destacadas) quando o produto atinge o nível de **Estoque Mínimo**.

---

### 🏗️ Arquitetura do Sistema



### 📂 Estrutura do Projeto

* **`/backend`**: Web API em ASP.NET Core com Entity Framework Core e PostgreSQL.
* **`backend/Estoque.Repositorio`**: Scripts de migração e modelos de dados do banco.
* **`/web`**: Painel de controle de estoque desenvolvido com React, Vite e Axios para consumo da API.


---

### ⚙️ Como Rodar o Projeto

**1. Pré-requisitos:**
* SDK do .NET 6+ instalado.
* Node.js e NPM instalados.
* Instância do PostgreSQL rodando localmente.

**2. Configuração do Banco:**
* Ajuste a `ConnectionString` no arquivo `appsettings.json` do backend para apontar para o seu banco.

**3. Execução:**

```bash
# Terminal 1 - Backend
cd backend
dotnet run

# Terminal 2 - Frontend
cd web
npm install
npm run dev

