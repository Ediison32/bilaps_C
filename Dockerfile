# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar la solución y proyectos
COPY ["webInmobiliary.sln", "./"]
COPY ["webInmobiliary.Api/webInmobiliary.Api.csproj", "webInmobiliary.Api/"]
COPY ["webInmobiliary.Application/webInmobiliary.Application.csproj", "webInmobiliary.Application/"]
COPY ["webInmobiliary.Domain/webInmobiliary.Domain.csproj", "webInmobiliary.Domain/"]
COPY ["webInmobiliary.Infrastructure/webInmobiliary.Infrastructure.csproj", "webInmobiliary.Infrastructure/"]

# Restaurar paquetes
RUN dotnet restore "webInmobiliary.sln"

# Copiar todo el código y publicar
COPY . .
WORKDIR "/src/webInmobiliary.Api"
RUN dotnet publish -c Release -o /app/publish

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "webInmobiliary.Api.dll"]
