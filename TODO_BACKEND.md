# Lista de Actividades Backend (MVP)

1. Validar estructura de Clean Architecture (`Domain`, `Application`, `Infrastructure`, `API`).
2. Configurar conexion a SQLite.
3. Configurar `DbContext` y crear migracion inicial.
4. Configurar autenticacion JWT (access token + refresh token).
5. Configurar login con Google.
6. Configurar autorizacion por roles (`Admin`, `Usuario`, `Reciclador`).
7. Crear modelo de `Usuario` (un solo rol activo a la vez).
8. Crear modelo de `BolsaReciclaje` (materiales, nivel, cantidad de bolsas, coordenada y descripcion del punto).
9. Crear modelo de `ReservaBolsa` (una sola reserva activa por bolsa).
10. Implementar estados de bolsa (`Creada`, `Disponible`, `Reservada`, `Recogida`, `Cancelada`).
11. Implementar regla de expiracion de reserva a 45 minutos.
12. Implementar `Register` (email + password).
13. Implementar `Login` (email + password).
14. Implementar `Login con Google`.
15. Implementar `Refresh Token`.
16. Implementar cambio de rol para usuario (solo 1 rol al tiempo).
17. Implementar crear bolsa de reciclaje.
18. Implementar listar bolsas por cercania (radio en km).
19. Implementar reservar bolsa.
20. Implementar cancelar reserva.
21. Implementar marcar bolsa como recogida.
22. Implementar control de concurrencia para evitar doble reserva.
23. Implementar notificaciones push instantaneas al reciclador cuando haya bolsa disponible.
24. Implementar modulo admin para configurar radio de busqueda en km.
25. Implementar modulo admin para configurar materiales del formulario.
26. Implementar modulo admin para configurar opciones de formularios.
27. Implementar almacenamiento de estadisticas base (donaciones por usuario y actividad del reciclador).
28. Exponer endpoints API para auth, bolsas, reservas y admin.
29. Agregar validaciones de entrada (DTOs + reglas de negocio).
30. Agregar manejo global de errores y logging.
31. Agregar rate limiting en endpoints sensibles.
32. Crear pruebas unitarias de dominio y casos de uso criticos.
33. Crear pruebas de integracion para auth y flujo de reservas.
34. Documentar API con Swagger/OpenAPI.

