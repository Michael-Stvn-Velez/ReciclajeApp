# Arquitectura Oficial del Proyecto

Este proyecto adopta **Clean Architecture** como arquitectura oficial.

Su cumplimiento es **obligatorio** y debe seguirse **al pie de la letra** en cualquier cambio, nueva funcionalidad, refactor o corrección.

## Principio Rector

La regla principal es la **Regla de Dependencias**:

- Las dependencias siempre apuntan **hacia adentro**.
- Las capas internas **no conocen** detalles de capas externas.
- Ninguna decisión técnica (framework, base de datos, proveedor externo) puede contaminar el dominio del negocio.

## Capas y Responsabilidades

### 1) Domain

- Contiene entidades, value objects, enums, reglas de negocio e invariantes.
- Es la capa más importante y debe ser independiente.
- **No** depende de Infrastructure, API, UI, ni frameworks.

### 2) Application

- Contiene casos de uso, comandos/queries, DTOs, validaciones e interfaces (puertos).
- Orquesta el flujo de negocio sin conocer implementaciones concretas.
- Solo puede depender de `Domain`.

### 3) Infrastructure

- Implementa detalles técnicos: persistencia, servicios externos, correo, cache, etc.
- Implementa interfaces definidas en `Application`.
- Puede depender de `Application` y `Domain`, nunca al revés.

### 4) Presentation (API)

- Expone endpoints/controladores.
- Debe ser delgada: recibe solicitudes, delega a casos de uso y responde.
- No debe contener lógica de negocio crítica.

## Reglas Obligatorias del Proyecto

1. **Prohibido** colocar lógica de negocio en controladores o en Infrastructure.
2. **Prohibido** que `Domain` dependa de librerías técnicas o frameworks.
3. **Prohibido** que `Application` dependa directamente de EF Core, HTTP u otros detalles de infraestructura.
4. Toda integración externa debe entrar por interfaces definidas en `Application` e implementadas en `Infrastructure`.
5. La inyección de dependencias y composición de módulos se realiza en la capa externa (API).
6. Cualquier cambio que viole estas reglas debe detenerse y rediseñarse antes de integrarse.

## Política de Cumplimiento

Este documento define la arquitectura oficial del proyecto.

Todo el equipo debe respetar estas reglas de manera estricta.
Si una propuesta o implementación no cumple con Clean Architecture, se considera inválida hasta corregirse.
