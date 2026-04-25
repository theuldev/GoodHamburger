
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER root
RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GoodHamburger.API/GoodHamburger.API.csproj", "GoodHamburger.API/"]
COPY ["GoodHamburger.Application/GoodHamburger.Application.csproj", "GoodHamburger.Application/"]
COPY ["GoodHamburger.Domain/GoodHamburger.Domain.csproj", "GoodHamburger.Domain/"]
COPY ["GoodHamburger.Infra/GoodHamburger.Infra.csproj", "GoodHamburger.Infra/"]
RUN dotnet restore "./GoodHamburger.API/GoodHamburger.API.csproj"
COPY . .
WORKDIR "/src/GoodHamburger.API"
RUN dotnet build "./GoodHamburger.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GoodHamburger.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoodHamburger.API.dll"]