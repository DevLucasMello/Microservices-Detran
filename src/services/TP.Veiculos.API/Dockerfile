#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./services/TP.Veiculos.API/TP.Veiculos.API.csproj", "services/TP.Veiculos.API/"]
COPY ["./services/TP.Veiculos.Application/TP.Veiculos.Application.csproj", "services/TP.Veiculos.Application/"]
COPY ["./services/TP.Veiculos.Domain/TP.Veiculos.Domain.csproj", "services/TP.Veiculos.Domain/"]
COPY ["./building blocks/TP.Core/TP.Core.csproj", "building blocks/TP.Core/"]
COPY ["./services/TP.Veiculos.Infra/TP.Veiculos.Infra.csproj", "services/TP.Veiculos.Infra/"]
COPY ["./building blocks/TP.MessageBus/TP.MessageBus.csproj", "building blocks/TP.MessageBus/"]
COPY ["./building blocks/TP.WebAPI.Core/TP.WebAPI.Core.csproj", "building blocks/TP.WebAPI.Core/"]
RUN dotnet restore "services/TP.Veiculos.API/TP.Veiculos.API.csproj"
COPY . .
WORKDIR "services/TP.Veiculos.API"
RUN dotnet build "TP.Veiculos.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TP.Veiculos.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TP.Veiculos.API.dll"]