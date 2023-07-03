# Imagem base para construir o projeto
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Diretório de trabalho
WORKDIR /app

# Copiar os arquivos do projeto
COPY . ./

# Restaurar dependências e compilar o projeto
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Imagem final para executar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Diretório de trabalho
WORKDIR /app

# Copiar os arquivos publicados do projeto
COPY --from=build-env /app/out .

# Porta exposta pela aplicação
EXPOSE 80

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "apiBank.dll"]
