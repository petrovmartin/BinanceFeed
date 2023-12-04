FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5273

ENV ASPNETCORE_URLS=http://+:5273

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Client/BinanceFeed.DataSeeder/BinanceFeed.DataSeeder.csproj", "Client/BinanceFeed.DataSeeder/"]
RUN dotnet restore "Client/BinanceFeed.DataSeeder/BinanceFeed.DataSeeder.csproj"
COPY . .
WORKDIR "/src/Client/BinanceFeed.DataSeeder"
RUN dotnet build "BinanceFeed.DataSeeder.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "BinanceFeed.DataSeeder.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BinanceFeed.DataSeeder.dll"]
