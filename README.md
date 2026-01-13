🏦 BancoSolution

Sistema bancario desarrollado como prueba técnica, que permite la gestión de clientes, cuentas, movimientos y la generación de reportes de estado de cuenta, siguiendo buenas prácticas de arquitectura backend con .NET 8, EF Core, Docker y principios de Clean Architecture.

🧱 Arquitectura

El proyecto está organizado en capas para garantizar mantenibilidad, testabilidad y separación de responsabilidades:
BancoSolution
│──src
│  ├── Banco.Api              → Capa de presentación (Controllers)
│  ├── Banco.Application      → Servicios, DTOs, interfaces
│  ├── Banco.Domain           → Entidades de dominio y reglas de negocio
│  └── Banco.Infrastructure   → EF Core, repositorios, UnitOfWork, PDF
│
├── BaseDatos.sql          → Script de creación de base de datos
├── Dockerfile
├── docker-compose.yaml
└── README.md

🚀 Tecnologías utilizadas
	•	.NET 8
	•	ASP.NET Core Web API
	•	Entity Framework Core
	•	SQL Server
	•	Docker & Docker Compose
	•	Postman
	•	C#
	•	Arquitectura en capas / Clean Architecture


🐳 Ejecución con Docker

1️⃣ Requisitos
	•	Docker
	•	Docker Compose


2️⃣ Levantar el proyecto

Desde la raíz del repositorio:
docker-compose up --build
Esto levantará:
	•	SQL Server en el puerto 1433
	•	API en el puerto 8080


3️⃣ Verificar que la API está activa

Abrir en el navegador:
http://localhost:8080
🗄️ Base de datos

La base de datos se llama: BaseDatos.sql
Incluye:
	•	Tabla Clientes
	•	Tabla Cuentas
	•	Tabla Movimientos
	•	Claves primarias y foráneas
	•	Índices únicos


📌 Endpoints disponibles

👤 Clientes

POST /api/clientes                       Crear cliente
PATCH /api/clientes/{clienteId}/estado   Activar / desactivar cliente
GET /api/clientes/{clienteId}/cuentas    Obtener cuentas del cliente

💳 Cuentas

POST /api/cuentas                         Crear cuenta 
GET /api/cuentas/{numeroCuenta}           Obtener cuenta
PATCH /api/cuentas/{numeroCuenta}/estado  Activar / desactivar cuenta

💰 Movimientos

POST /api/movimientos  Registrar depósito o retiro

📄 Reportes

GET /api/reportes  Generar estado de cuenta por rango de fechas

🧠 Decisiones de diseño destacadas
	•	Los movimientos no se modifican ni eliminan (inmutables)
	•	Separación clara entre:
	•	Controllers
	•	Services
	•	Repositories
	•	Uso de Unit of Work
	•	Validaciones de negocio en la capa de aplicación
	•	Manejo correcto de rangos de fechas
	•	Reportes financieros robustos
	•	Generación de reporte en formato Base64


🧪 Pruebas

Los endpoints pueden ser probados mediante Postman.

Se recomienda el siguiente flujo:
	1.	Crear cliente
	2.	Crear cuenta
	3.	Registrar movimientos
	4.	Generar reporte

👨‍💻 Autor

Victor Cadena
FullStack Developer – .NET / Angular
