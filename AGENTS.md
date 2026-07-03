# Contexto del Proyecto
Este proyecto es una aplicación para administrar un Backend para un Single Sign On (SSO) 

## 1. Contexto Tecnológico
- **Lenguaje:** C# 12
- **Frameworks:** .NET 8 (o tu versión correspondiente)
- **Patrones de Arquitectura:** Clean Architecture, Domain-Driven Design (DDD), Inyección de Dependencias.
- **ORM:** Dapper

## 2. Convenciones de Código y Estilo
- Sigue siempre las convenciones oficiales de Microsoft para C#.
- Usa **PascalCase** para nombres de clases, métodos y propiedades.
- Usa **camelCase** para parámetros y variables locales.
- Evita el uso de variables implícitas (`var`) a menos que el tipo sea evidente en la misma línea.
- Las interfaces deben comenzar con la letra `I` (ej. `IRepository`).
- Todos los servicios deben ser inyectados mediante inyección de dependencia en el constructor.
- Documenta clases y métodos públicos utilizando comentarios XML (`/// <summary>`).

## 3. Estructura de Proyectos
Asume la siguiente estructura de solución:
- `[NombreProyecto].Domain`: Entidades base y reglas de negocio.
- `[NombreProyecto].Application`: Casos de uso e interfaces de repositorios.
- `[NombreProyecto].Infrastructure`: Implementación de EF Core, migraciones y servicios externos.
- `[NombreProyecto].API`: Controladores, middlewares y configuración de la API.
- `[NombreProyecto].Web`: Sitio Web para administrar la aplicación

## 4. Restricciones
- No expongas entidades del Dominio directamente en las respuestas de la API; utiliza DTOs (Data Transfer Objects).
- No uses `System.Console.WriteLine` para logs. Utiliza siempre `ILogger<T>`.
- Evita llamadas asíncronas bloqueantes (no uses `.Result` o `.Wait()`); utiliza siempre `await`.

## 5. Comandos y Verificaciones (CLI)
Antes de declarar una tarea como terminada, el agente debe ejecutar los siguientes comandos en la terminal desde la raíz del proyecto:
- Restaurar dependencias: `dotnet restore`
- Construir solución: `dotnet build`
- Ejecutar pruebas: `dotnet test`
