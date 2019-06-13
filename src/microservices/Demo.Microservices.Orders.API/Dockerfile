FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Demo.Microservices.Orders.API/Demo.Microservices.Orders.API.csproj", "Demo.Microservices.Orders.API/"]
RUN dotnet restore "Demo.Microservices.Orders.API/Demo.Microservices.Orders.API.csproj"
COPY . .
WORKDIR "/src/Demo.Microservices.Orders.API"
RUN dotnet build "Demo.Microservices.Orders.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Demo.Microservices.Orders.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Demo.Microservices.Orders.API.dll"]