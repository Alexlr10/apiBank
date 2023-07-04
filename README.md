# API Bank
Este é um projeto de API em C# com .NET Core que simula algumas funcionalidades de um banco digital. A API utiliza arquitetura GraphQL e é executada em um ambiente de desenvolvimento ASP.NET Core. O banco de dados utilizado é o MySQL.

## Requisitos de Software
Antes de prosseguir com a execução da aplicação, verifique se você possui os seguintes pré-requisitos de software instalados:

- Visual Studio 2022: Um ambiente de desenvolvimento integrado (IDE) para o desenvolvimento de aplicativos ASP.NET Core. Você pode baixar o Visual Studio 2022 Community gratuitamente em [https://visualstudio.microsoft.com/pt-br/vs/community/](https://visualstudio.microsoft.com/pt-br/vs/community/).

- MySQL Server: Um sistema de gerenciamento de banco de dados relacional. Você pode baixar o MySQL Server em [https://dev.mysql.com/downloads/mysql/](https://dev.mysql.com/downloads/mysql/) e seguir as instruções de instalação fornecidas.

- MySQL Workbench ou DBeaver: Uma ferramenta de interface gráfica para gerenciamento de bancos de dados MySQL. Você pode baixar o MySQL Workbench em [https://www.mysql.com/products/workbench](https://www.mysql.com/products/workbench) ou o DBeaver em [https://dbeaver.io](https://dbeaver.io) e seguir as instruções de instalação fornecidas.

## Configuração
As etapas de configuração são as seguintes:

1. Clone este repositório para o seu ambiente local: `git clone https://github.com/Alexlr10/apiBank.git`

2. Abra o projeto "Bank.sln" no Visual Studio 2022.

3. No arquivo ".env" na raiz do projeto, atualize as seguintes variáveis de ambiente com as credenciais de acesso ao banco de dados mysql:

DB_SERVER=nome_do_servidor

DB_DATABASE=bancodigital

DB_USER=nome_do_usuario

DB_PASSWORD=senha_do_usuario


# Executando a Aplicação sem Docker e Importando o Banco de Dados

Certifique-se de ter o Visual Studio 2022 instalado e o projeto "apiBank" selecionado como o projeto de inicialização.

1. Abra a ferramenta de interface do MySQL (por exemplo, MySQL Workbench ou DBeaver).

2. Crie um novo banco de dados chamado "bancodigital" de acordo com as credencias do .env.

3. Importe o arquivo "script.sql" fornecido no repositório para o banco de dados "bancodigital".

4. Pressione F5 ou clique em "Iniciar Depuração" no Visual Studio 2022 para iniciar a aplicação.

5. Acesse `https://localhost:50648/graphql` em seu navegador para acessar a interface do GraphQL, onde você pode executar as consultas e mutações disponíveis.

Dessa forma, você primeiro cria o banco de dados e importa o arquivo SQL antes de iniciar a aplicação no Visual Studio 2022. Isso garante que o banco de dados necessário esteja configurado antes que a aplicação seja executada.

Observação: As etapas acima pressupõem que você possui todas as dependências e configurações adequadas para executar o projeto "apiBank" sem o Docker. Certifique-se de seguir as instruções fornecidas com o projeto para configurar corretamente o ambiente de execução.


## Executando a Aplicação com o Docker
Para executar a aplicação com o Docker, siga as etapas abaixo:

1. Certifique-se de ter o Docker instalado em seu computador. Você pode baixar o Docker em [https://www.docker.com](https://www.docker.com) e seguir as instruções de instalação fornecidas.

2. No terminal, navegue até o diretório raiz do projeto.

3. Execute o comando a seguir para criar e executar os containers Docker: `docker-compose up`, ele irar fazer o download de todas as dependencias necessarias incluindo o bando de dados.

4. Aguarde até que o processo de criação e inicialização dos containers seja concluído.

5. Acesse `https://localhost/graphql` em seu navegador para acessar a interface do GraphQL, onde você pode executar as consultas e mutações disponíveis.

## Querys e Mutações disponiveis
```graphql
mutation UpsertConta {
  upsertConta ( request : {
    conta : "000006",
    saldo : 1000
  }) {
    payload {
      id,
      conta,
      saldo
    }
    errors{
      errorCode,
      errorMessage,
      propertyName,
      severity
    }
  } 
}

mutation SaqueEmConta {
  saqueEmConta ( request : {
    conta : "123456789",
    valor : 10000
  }) {
    payload {
      conta,
      saldo
    }
    errors{
      errorCode,
      errorMessage,
      propertyName,
      severity
    }
  } 
}

mutation DepositoEmConta {
  depositoEmConta( request : {
    conta : "123456789",
    valor : 1000
  }) {
    payload {
      conta,
      saldo
    }
    errors{
      errorCode,
      errorMessage,
      propertyName,
      severity
    }
  } 
}

query getContas {
  contas {
    payload{
      id,
      conta,
      saldo
    }
  }
}

query getConta {
  conta( request: {id : "2a6713c1-8e71-4a0f-bc13-63d199bc6b5a" }) {
    payload{
      id,
      conta,
      saldo
    }
  }
}

query getSaldo {
  saldo(request: { conta : "123456789" }) {
    payload{
      saldo
    }
  }
}
```

## Executando os Testes Unitários
O projeto "TestApiBank" contém os testes unitários para a aplicação. Para executá-los, siga as etapas abaixo:

Abra o projeto "TestApiBank" no Visual Studio 2022.

Certifique-se de ter selecionado o projeto "TestApiBank.csproj" como projeto de inicialização.

No menu "Teste", clique em "Executar" e selecione "Todos os Testes" para executar todos os testes unitários.

Os resultados dos testes serão exibidos no "Gerenciador de Testes" no Visual Studio.

Após seguir essas etapas, você terá a aplicação ASP.NET Core "apiBank" em execução, juntamente com os testes unitários sendo executados para validar o seu funcionamento.






