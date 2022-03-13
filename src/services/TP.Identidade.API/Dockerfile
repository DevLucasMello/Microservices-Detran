#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./services/TP.Identidade.API/TP.Identidade.API.csproj", "services/TP.Identidade.API/"]
COPY ["./building blocks/TP.MessageBus/TP.MessageBus.csproj", "building blocks/TP.MessageBus/"]
COPY ["./building blocks/TP.Core/TP.Core.csproj", "building blocks/TP.Core/"]
COPY ["./building blocks/TP.WebAPI.Core/TP.WebAPI.Core.csproj", "building blocks/TP.WebAPI.Core/"]
RUN dotnet restore "services/TP.Identidade.API/TP.Identidade.API.csproj"
COPY . .
WORKDIR "services/TP.Identidade.API"
RUN dotnet build "TP.Identidade.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TP.Identidade.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TP.Identidade.API.dll"]