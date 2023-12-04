FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5274

ENV ASPNETCORE_URLS=http://+:5274

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Client/BinanceFeed.Console/BinanceFeed.Console.csproj", "Client/BinanceFeed.Console/"]
RUN dotnet restore "Client\BinanceFeed.Console\BinanceFeed.Console.csproj"
COPY . .
WORKDIR "/src/Client/BinanceFeed.Console"
RUN dotnet build "BinanceFeed.Console.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "BinanceFeed.Console.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BinanceFeed.Console.dll"]
