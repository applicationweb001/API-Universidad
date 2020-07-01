FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Sistema.Web/Sistema.Web.csproj", "Sistema.Web/"]
COPY ["Sistema.Entidades/Sistema.Entidades.csproj", "Sistema.Entidades/"]
COPY ["Sistema.Datos/Sistema.Datos.csproj", "Sistema.Datos/"]
RUN dotnet restore "Sistema.Web/Sistema.Web.csproj"
COPY . .
WORKDIR "/src/Sistema.Web"
RUN dotnet build "Sistema.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sistema.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sistema.Web.dll"]