FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["BookStoreApi.csproj", "./"]
RUN dotnet restore "BookStoreApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BookStoreApi.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "BookStoreApi.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookStoreApi.dll"]
