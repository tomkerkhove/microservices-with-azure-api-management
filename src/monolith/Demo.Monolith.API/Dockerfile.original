#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Demo.Monolith.API/Demo.Monolith.API.csproj", "Demo.Monolith.API/"]
RUN dotnet restore "Demo.Monolith.API/Demo.Monolith.API.csproj"
COPY . .
WORKDIR "/src/Demo.Monolith.API"
RUN dotnet build "Demo.Monolith.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.Monolith.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.Monolith.API.dll"]