FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Demo.Microservices.Shipments.API/Demo.Microservices.Shipments.API.csproj", "Demo.Microservices.Shipments.API/"]
RUN dotnet restore "Demo.Microservices.Shipments.API/Demo.Microservices.Shipments.API.csproj"
COPY . .
WORKDIR "/src/Demo.Microservices.Shipments.API"
RUN dotnet build "Demo.Microservices.Shipments.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Demo.Microservices.Shipments.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Demo.Microservices.Shipments.API.dll"]