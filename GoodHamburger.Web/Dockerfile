FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["GoodHamburger.Web/GoodHamburger.Web.csproj", "GoodHamburger.Web/"]
RUN dotnet restore "GoodHamburger.Web/GoodHamburger.Web.csproj"
COPY . .
WORKDIR "/src/GoodHamburger.Web"
RUN dotnet build "GoodHamburger.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoodHamburger.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY GoodHamburger.Web/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
