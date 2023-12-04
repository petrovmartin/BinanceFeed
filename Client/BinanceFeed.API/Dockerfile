FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5272

ENV ASPNETCORE_URLS=http://+:5272

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Client/BinanceFeed.API/BinanceFeed.API.csproj", "Client/BinanceFeed.API/"]
RUN dotnet restore "Client/BinanceFeed.API/BinanceFeed.API.csproj"
COPY . .
WORKDIR "/src/Client/BinanceFeed.API"
RUN dotnet build "BinanceFeed.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "BinanceFeed.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BinanceFeed.API.dll"]
