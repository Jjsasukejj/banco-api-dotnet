# Banco API - Prueba Técnica

Este proyecto corresponde a una prueba técnica Full Stack para la implementación de un sistema bancario básico.

## 🛠 Tecnologías
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- xUnit
- Docker

## 📦 Arquitectura
El proyecto está estructurado siguiendo principios de Clean Architecture:

- Banco.Api
- Banco.Application
- Banco.Domain
- Banco.Infrastructure
- Banco.Tests

## 🚀 Funcionalidades
- Gestión de clientes
- Gestión de cuentas bancarias
- Registro de movimientos (créditos y débitos)
- Validaciones de saldo y cupo diario
- Generación de reportes por rango de fechas
- Exportación de reportes en JSON y PDF

## ▶️ Ejecución
```bash
dotnet build
dotnet run --project Banco.Api
