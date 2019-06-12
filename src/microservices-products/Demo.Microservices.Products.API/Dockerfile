FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Demo.Microservices.Products.API/Demo.Microservices.Products.API.csproj", "Demo.Microservices.Products.API/"]
RUN dotnet restore "Demo.Microservices.Products.API/Demo.Microservices.Products.API.csproj"
COPY . .
WORKDIR "/src/Demo.Microservices.Products.API"
RUN dotnet build "Demo.Microservices.Products.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Demo.Microservices.Products.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Demo.Microservices.Products.API.dll"]