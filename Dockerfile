# =========================
# Esta imagen se usa para ejecutar la api
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Exponemos el puerto interno del contenedor donde estara escuchando la api
EXPOSE 80

# =========================
# Esta imagen compila y publica el proyecto que incluye sdk
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos todo el repositorio al contenedor para poder compilar
COPY . .

# Restauramos dependencias apuntando a la capa de Api
RUN dotnet restore src/Banco.Api/Banco.Api.csproj

# Publicamos en Release para que el runtime solo copie binarios finales
RUN dotnet publish src/Banco.Api/Banco.Api.csproj -c Release -o /app/publish

# =========================
# Copiamos lo publicado a la imagen runtime y arrancamos la api
# =========================
FROM base AS final
WORKDIR /app

# Copiamos los binarios compilados desde la etapa build
COPY --from=build /app/publish .

# Comando de inicio del contenedor
ENTRYPOINT ["dotnet", "Banco.Api.dll"]