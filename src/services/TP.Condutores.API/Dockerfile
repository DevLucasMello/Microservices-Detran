#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./services/TP.Condutores.API/TP.Condutores.API.csproj", "services/TP.Condutores.API/"]
COPY ["./building blocks/TP.MessageBus/TP.MessageBus.csproj", "building blocks/TP.MessageBus/"]
COPY ["./building blocks/TP.Core/TP.Core.csproj", "building blocks/TP.Core/"]
COPY ["./building blocks/TP.WebAPI.Core/TP.WebAPI.Core.csproj", "building blocks/TP.WebAPI.Core/"]
COPY ["./services/TP.Condutores.Application/TP.Condutores.Application.csproj", "services/TP.Condutores.Application/"]
COPY ["./services/TP.Condutores.Domain/TP.Condutores.Domain.csproj", "services/TP.Condutores.Domain/"]
COPY ["./services/TP.Condutores.Infra/TP.Condutores.Infra.csproj", "services/TP.Condutores.Infra/"]
RUN dotnet restore "services/TP.Condutores.API/TP.Condutores.API.csproj"
COPY . .
WORKDIR "services/TP.Condutores.API"
RUN dotnet build "TP.Condutores.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TP.Condutores.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TP.Condutores.API.dll"]