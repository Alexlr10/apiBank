# API Bank
Este é um projeto de API em C# com .NET Core que simula algumas funcionalidades de um banco digital. A API utiliza arquitetura GraphQL e é executada em um ambiente de desenvolvimento ASP.NET Core. O banco de dados utilizado é o MySQL.

## Requisitos de Software
Antes de prosseguir com a execução da aplicação, verifique se você possui os seguintes pré-requisitos de software instalados:

- Visual Studio 2022: Um ambiente de desenvolvimento integrado (IDE) para o desenvolvimento de aplicativos ASP.NET Core. Você pode baixar o Visual Studio 2022 Community gratuitamente em [https://visualstudio.microsoft.com/pt-br/vs/community/](https://visualstudio.microsoft.com/pt-br/vs/community/).

- MySQL Server: Um sistema de gerenciamento de banco de dados relacional. Você pode baixar o MySQL Server em [https://dev.mysql.com/downloads/mysql/](https://dev.mysql.com/downloads/mysql/) e seguir as instruções de instalação fornecidas.

- MySQL Workbench ou DBeaver: Uma ferramenta de interface gráfica para gerenciamento de bancos de dados MySQL. Você pode baixar o MySQL Workbench em [https://www.mysql.com/products/workbench](https://www.mysql.com/products/workbench) ou o DBeaver em [https://dbeaver.io](https://dbeaver.io) e seguir as instruções de instalação fornecidas.

## Configuração
As etapas de configuração são as seguintes:

1. Clone este repositório para o seu ambiente local: git clone https://github.com/Alexlr10/apiBank.git

2. Abra o projeto "Bank.sln" no Visual Studio 2022.

3. No arquivo ".env" na raiz do projeto, atualize as seguintes variáveis de ambiente com as credenciais de acesso ao banco de dados mysql, caso utilize o docker atualize as variaveis tambem no arquivo docker-compose.yaml:

DB_SERVER=nome_do_servidor
DB_DATABASE=bancodigital
DB_USER=nome_do_usuario
DB_PASSWORD=senha_do_usuario


## Importando o Banco de Dados ( Somente se for rodar o projeto sem Docker)
As etapas para importar o banco de dados são as seguintes:

1. Abra a ferramenta de interface do MySQL (por exemplo, MySQL Workbench ou DBeaver).

2. Crie um novo banco de dados chamado "bancodigital".

3. Importe o arquivo "script.sql" fornecido no repositório para o banco de dados "bancodigital".

## Executando a Aplicação sem Docker
Para executar a aplicação sem o Docker, siga as etapas abaixo:

1. No Visual Studio 2022, certifique-se de ter selecionado o projeto "apiBank" como projeto de inicialização.

2. Pressione F5 ou clique em "Iniciar Depuração" para iniciar a aplicação.

3. Acesse `https://localhost:50648/graphql` em seu navegador para acessar a interface do GraphQL, onde você pode executar as consultas e mutações disponíveis.

## Executando a Aplicação com o Docker
Para executar a aplicação com o Docker, siga as etapas abaixo:

1. Certifique-se de ter o Docker instalado em seu computador. Você pode baixar o Docker em [https://www.docker.com](https://www.docker.com) e seguir as instruções de instalação fornecidas.

2. No terminal, navegue até o diretório raiz do projeto.

3. Execute o comando a seguir para criar e executar os containers Docker: `docker-compose up --build`, ele irar fazer o download de todas as dependencias necessarias incluindo o bando de dados.

4. Aguarde até que o processo de criação e inicialização dos containers seja concluído.

5. Acesse `https://localhost/graphql` em seu navegador para acessar a interface do GraphQL, onde você pode executar as consultas e mutações disponíveis.

## Executando os Testes Unitários
O projeto "TestApiBank" contém os testes unitários para a aplicação. Para executá-los, siga as etapas abaixo:

Abra o projeto "TestApiBank" no Visual Studio 2022.

Certifique-se de ter selecionado o projeto "TestApiBank.csproj" como projeto de inicialização.

No menu "Teste", clique em "Executar" e selecione "Todos os Testes" para executar todos os testes unitários.

Os resultados dos testes serão exibidos no "Gerenciador de Testes" no Visual Studio.

Após seguir essas etapas, você terá a aplicação ASP.NET Core "apiBank" em execução, juntamente com os testes unitários sendo executados para validar o seu funcionamento.






