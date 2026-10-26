---
title: "Sistema de Paseadores de Perros"
subtitle: "Trabajo Práctico Integrador — Entrega final"
lang: es-AR
---

# Carátula

| | |
|---|---|
| **Institución** | Universidad Tecnológica Nacional — Facultad Regional Rosario |
| **Asignatura** | Tecnologías de Desarrollo de Software IDE |
| **Comisión** | 3EK02 |
| **Ciclo lectivo** | 2026 |
| **Docentes** | Ezequiel Porta |
| | Sebastián Rocco |
| **Grupo** | 7 |
| **Integrantes** | Dentesano Valentino (legajo 54915) — dentesanovalentino@gmail.com |
| | Giunta Lautaro (legajo 54714) — lautogiunta@gmail.com |
| | Taborda Fausto (legajo 54914) — fausto.taborda05@gmail.com |
| **Repositorio** | <https://github.com/LautaroGiunta/tp-net-paseadores-mascotas> |

# 1. Descripción general

El sistema administra un servicio de paseo de perros. Hay tres tipos de usuarios:

- **Administrador**: gestiona todo el sistema (usuarios, perros, paseos, liquidaciones y reportes).
- **Dueño**: el cliente. Registra a sus perros y pide paseos para ellos.
- **Paseador**: presta el servicio. Trabaja en una zona y cobra una tarifa por hora; registra los paseos que hace.

Cada **paseo** une a un paseador con un perro en una fecha y hora de inicio y con una duración. El precio
no lo carga el usuario: el sistema lo calcula con la tarifa del paseador en ese momento y lo deja fijo.

Periódicamente el administrador arma una **liquidación** para cada paseador: elige un período y el sistema
le muestra los paseos terminados que todavía no se le pagaron. La liquidación (maestro) agrupa esas líneas
(detalle), cada una con el importe del paseo congelado, y el total es la suma de las líneas.

Además, el administrador consulta dos **reportes**: la recaudación mensual por paseador (con gráfico) y la
actividad de cada perro en un período.

El sistema tiene dos interfaces que usan la misma API: una aplicación de **escritorio** (Windows Forms) y
una aplicación **web** (Blazor WebAssembly). Las dos piden usuario y contraseña y muestran solo las opciones
que el rol del usuario puede usar.

## 1.1 Reglas de negocio

**Usuarios (administradores, dueños y paseadores)**

- El email identifica al usuario en el login: no puede haber dos usuarios con el mismo email, y tiene que
  tener formato válido.
- Nombre, apellido, email y teléfono son obligatorios. El teléfono tiene que tener entre 8 y 15 dígitos,
  sin espacios ni letras.
- La contraseña tiene que tener al menos 8 caracteres. Se guarda hasheada con BCrypt, nunca en texto plano,
  y nunca se devuelve al cliente. Al modificar un usuario, si la contraseña se deja vacía se conserva la
  anterior.
- La fecha de alta la asigna el sistema al crear el registro y no se modifica después.
- El dueño tiene que tener dirección. El paseador tiene que tener zona y una tarifa por hora mayor que cero.
- Un administrador no puede eliminarse a sí mismo (así siempre queda al menos uno).
- No se puede eliminar un dueño que tenga perros, ni un paseador que tenga paseos.

**Perros**

- Todo perro pertenece a un dueño existente. Nombre y raza son obligatorios y la edad va de 0 a 30 años.
- No se puede eliminar un perro que tenga paseos registrados.
- Un dueño solo puede cargar, modificar o eliminar sus propios perros.

**Paseos**

- No se puede agendar un paseo en una fecha y hora que ya pasó.
- La duración va de 15 a 480 minutos.
- Un paseador no puede tener dos paseos que se superpongan en el tiempo.
- El precio se calcula como `tarifa por hora × duración / 60` (redondeado a 2 decimales) y queda fijo
  aunque después cambie la tarifa.
- Un dueño solo puede pedir y gestionar paseos de sus perros; un paseador solo los suyos.
- Un paseo que ya está en una liquidación no se puede modificar ni eliminar.

**Liquidaciones (maestro/detalle)**

- Solo pueden incluir paseos del paseador de la liquidación, dentro del período elegido y ya terminados.
- Un paseo se le paga una sola vez: no puede estar en dos liquidaciones (índice único en la base).
- Una liquidación tiene que tener al menos un paseo.
- El importe de cada línea se copia del precio del paseo al liquidarlo; el total no se guarda, se calcula
  sumando las líneas.
- Si se elimina una liquidación, se eliminan sus líneas y esos paseos vuelven a quedar pendientes.

**Reportes**

- Solo cuentan los paseos ya terminados (los agendados todavía no se cobraron).

## 1.2 Permisos por rol

| Operación | Admin | Dueño | Paseador |
|---|:-:|:-:|:-:|
| Consultar dueños, paseadores, perros y paseos | sí | sí | sí |
| Alta, baja y modificación de dueños | sí | no | no |
| Alta, baja y modificación de paseadores | sí | no | no |
| Alta, baja y modificación de perros | sí | sí (los suyos) | no |
| Alta, baja y modificación de paseos | sí | sí (de sus perros) | sí (los suyos) |
| Administradores (todo) | sí | no | no |
| Liquidaciones (todo) | sí | no | no |
| Reportes | sí | no | no |

La API rechaza con 401 los pedidos sin token y con 403 los que el rol no permite. Las pantallas esconden las
opciones que el rol no puede usar.

# 2. Requerimientos

## 2.1 Requerimientos funcionales

| Id | Requerimiento |
|---|---|
| RF01 | Iniciar sesión con email y contraseña; el sistema identifica el rol del usuario. |
| RF02 | Cerrar sesión. |
| RF03 | ABMC de dueños (nombre, apellido, email, teléfono, dirección, contraseña). |
| RF04 | ABMC de paseadores (nombre, apellido, email, teléfono, zona, tarifa por hora, contraseña). |
| RF05 | Buscar paseadores por texto (nombre, apellido, email o zona). Disponible en la web. |
| RF06 | ABMC de perros (dueño, nombre, raza, edad). |
| RF07 | ABMC de paseos (paseador, perro, fecha y hora de inicio, duración); el precio lo calcula el sistema. |
| RF08 | Buscar paseos filtrando por paseador y por rango de fechas. |
| RF09 | ABMC de administradores. |
| RF10 | ABMC de liquidaciones a paseadores (maestro/detalle): elegir paseador y período, listar los paseos pendientes de pago, agregarlos o quitarlos del detalle y ver el total. |
| RF11 | Reporte de recaudación mensual por paseador, con gráfico de barras y tabla, para un período. |
| RF12 | Reporte de actividad por perro (cantidad de paseos, minutos, gasto y último paseo) para un período, incluyendo los perros sin paseos. |
| RF13 | Restringir las operaciones y el menú según el rol del usuario (ver 1.2). |
| RF14 | Los ABMC, la búsqueda de paseos, las liquidaciones y los reportes están disponibles en la aplicación de escritorio y en la web. |

## 2.2 Requerimientos no funcionales

| Id | Requerimiento |
|---|---|
| RNF01 | **Arquitectura**: solución en N-capas (presentación, API, servicios, datos, dominio) con DTOs entre capas. |
| RNF02 | **Plataforma**: .NET 8, C#. API con ASP.NET Core; escritorio con Windows Forms; web con Blazor WebAssembly. |
| RNF03 | **Persistencia**: SQL Server. Entity Framework Core (code first, migraciones) para los ABMC y ADO.NET para los reportes. |
| RNF04 | **Base autogenerada**: al arrancar, la API aplica las migraciones y carga datos de prueba si la base está vacía. |
| RNF05 | **Seguridad**: autenticación con token JWT (120 minutos) y autorización por rol en cada endpoint. Contraseñas hasheadas con BCrypt. Consultas SQL parametrizadas (sin concatenar texto). |
| RNF06 | **Validación**: las entidades del dominio validan sus datos y no pueden quedar en un estado inválido; las pantallas avisan antes de enviar y muestran los errores que devuelve la API. |
| RNF07 | **Integridad**: claves foráneas con borrado restringido, salvo las líneas de una liquidación (en cascada con su cabecera). |
| RNF08 | **Configuración**: la URL de la API se toma de `appsettings.json` en escritorio y en web; la clave JWT, la cadena de conexión y los orígenes CORS, del `appsettings.json` de la API. |
| RNF09 | **Documentación de la API**: Swagger con soporte para probar con token. |
| RNF10 | **Usabilidad**: interfaz en español, mensajes de error claros y gráfico de los reportes sin depender de internet (Chart.js incluido en el proyecto). |

# 3. Capturas de pantalla

Capturas tomadas con los datos de prueba, ingresando con cada uno de los tres roles: administrador (admin@paseos.com), dueño (ana.gomez@paseos.com) y paseador (carlos.ruiz@paseos.com).

## 3.1 Aplicación de escritorio

El menú de la ventana principal muestra solo las opciones que el rol puede usar. Cada pantalla de ABMC tiene el formulario de carga a la izquierda y la grilla a la derecha; con doble clic en una fila se carga para modificarla.

![Escritorio: inicio de sesión](capturas/esc-01-login.png)

![Escritorio: menú Gestión del administrador](capturas/esc-menu-admin.png)

![Escritorio: menú Reportes del administrador](capturas/esc-menu-admin-reportes.png)

![Escritorio: menú del dueño (solo Perros y Paseos)](capturas/esc-menu-dueno.png)

![Escritorio: menú del paseador (solo Paseos)](capturas/esc-menu-paseador.png)

![Escritorio: ABMC de dueños](capturas/esc-duenos-admin.png)

![Escritorio: ABMC de paseadores](capturas/esc-paseadores-admin.png)

![Escritorio: ABMC de perros (administrador)](capturas/esc-perros-admin.png)

![Escritorio: perros vistos por un dueño (solo los suyos, con el dueño fijo)](capturas/esc-perros-dueno.png)

![Escritorio: ABMC de paseos (administrador)](capturas/esc-paseos-admin.png)

![Escritorio: búsqueda de paseos filtrando por paseador y rango de fechas](capturas/esc-paseos-admin-filtro.png)

![Escritorio: paseos vistos por un dueño (solo los de sus perros)](capturas/esc-paseos-dueno.png)

![Escritorio: paseos vistos por un paseador (solo los suyos)](capturas/esc-paseos-paseador.png)

![Escritorio: ABMC de administradores](capturas/esc-administradores-admin.png)

![Escritorio: liquidaciones registradas (maestro)](capturas/esc-liquidaciones-lista.png)

![Escritorio: edición de una liquidación (cabecera, paseos pendientes y detalle con el total)](capturas/esc-liquidaciones-detalle.png)

## 3.2 Aplicación web

La barra lateral muestra solo las páginas que el rol puede usar; las páginas solo para el administrador redirigen al inicio si otro rol entra por URL.

![Web: inicio de sesión](capturas/web-01-login.png)

![Web: menú del administrador](capturas/web-inicio-admin.png)

![Web: menú del dueño](capturas/web-inicio-dueno.png)

![Web: menú del paseador](capturas/web-inicio-paseador.png)

![Web: ABMC de dueños](capturas/web-duenos-admin.png)

![Web: ABMC de paseadores](capturas/web-paseadores-admin.png)

![Web: ABMC de perros (administrador)](capturas/web-perros-admin.png)

![Web: perros vistos por un dueño](capturas/web-perros-dueno.png)

![Web: ABMC de paseos (administrador)](capturas/web-paseos-admin.png)

![Web: búsqueda de paseos filtrando por paseador y rango de fechas](capturas/web-paseos-admin-filtro.png)

![Web: paseos vistos por un dueño](capturas/web-paseos-dueno.png)

![Web: paseos vistos por un paseador](capturas/web-paseos-paseador.png)

![Web: ABMC de administradores](capturas/web-administradores-admin.png)

![Web: liquidaciones (maestro/detalle) editando una liquidación](capturas/web-liquidaciones-detalle.png)

## 3.3 Reportes

Los dos reportes se consultan con ADO.NET y solo los ve el administrador. La recaudación mensual incluye un gráfico de barras (ScottPlot en escritorio, Chart.js en web).

![Escritorio: reporte de recaudación mensual por paseador](capturas/esc-reporterecaudacion-admin.png)

![Escritorio: reporte de actividad por perro](capturas/esc-reporteactividad-admin.png)

![Web: reporte de recaudación mensual por paseador](capturas/web-reportes-recaudacion-admin.png)

![Web: reporte de actividad por perro](capturas/web-reportes-actividad-admin.png)

# 4. Modelo de datos

La base de datos (`PaseadoresDB`, SQL Server) la genera Entity Framework Core a partir de las migraciones
del proyecto `Data` (`InicialHerencia`, `SeedUsuariosIniciales`, `AgregarLiquidaciones` y
`HashearContrasenas`).

![Modelo de datos](diagramas/modelo-datos.png)

**Herencia TPT (tabla por tipo).** `Usuarios` guarda los datos comunes a todos los usuarios (incluido el rol).
`Duenos` y `Paseadores` guardan solo los datos propios de cada tipo y su clave primaria `Id` es a la vez
clave foránea a `Usuarios.Id`. Los administradores no tienen datos extra, así que solo tienen su fila en
`Usuarios` (con `Rol = 0`).

| Tabla | Contenido | Relaciones |
|---|---|---|
| Usuarios | Datos comunes: nombre, apellido, email, teléfono, contraseña (hash), rol y fecha de alta. | Base de la herencia. |
| Duenos | Dirección. | `Id` → `Usuarios.Id` (cascade). |
| Paseadores | Zona y tarifa por hora. | `Id` → `Usuarios.Id` (cascade). |
| Perros | Nombre, raza, edad y fecha de alta. | `DuenoId` → `Duenos.Id` (restrict). |
| Paseos | Inicio, duración, precio total y fecha de alta. | `PaseadorId` → `Paseadores.Id`, `PerroId` → `Perros.Id` (restrict). |
| Liquidaciones | Cabecera: período (desde/hasta) y fecha de alta. | `PaseadorId` → `Paseadores.Id` (restrict). |
| LiquidacionDetalles | Línea: paseo liquidado e importe congelado. | `LiquidacionId` → `Liquidaciones.Id` (cascade), `PaseoId` → `Paseos.Id` (restrict, índice único). |

Los importes (`TarifaPorHora`, `PrecioTotal`, `Importe`) son `decimal(10,2)`. La fecha de fin del paseo y
el total de la liquidación no se guardan: se calculan.

# 5. Modelo de clases

Clases del proyecto `Domain.Model`. Todas las propiedades tienen `private set`: los datos solo se cambian a
través de métodos `Set…` que validan, así una entidad no puede quedar en un estado inválido.

![Modelo de clases](diagramas/modelo-clases.png)

- **Usuario** es la clase base de **Dueno** y **Paseador**; el rol se guarda en el enum **RolUsuario**. Un
  administrador es un `Usuario` con rol `Admin`.
- **Paseador.CalcularPrecio** calcula el precio de un paseo según su tarifa.
- **Paseo.CalcularFechaHoraFin** deduce el fin a partir del inicio y la duración.
- **Liquidacion** es la raíz del maestro/detalle: sus líneas (**LiquidacionDetalle**) solo se agregan o
  quitan con `AgregarPaseo` y `QuitarPaseo`, que validan las reglas (paseador, período, paseo terminado, sin
  repetir). `CalcularTotal` suma las líneas.
- **PaseoCriteria** y **PaseadorCriteria** son los filtros de las búsquedas.

Este modelo reemplaza al modelo de dominio preliminar: las clases `Persona`, `Direccion`, `Zona`, `Email`,
`Precio` y `EstadoPaseo` no se implementaron (dirección, zona y email quedaron como texto, el precio como
`decimal` y el estado del paseo se deduce de su fecha), y se agregaron `Liquidacion` y `LiquidacionDetalle`.

# 6. Arquitectura implementada

![Arquitectura implementada](diagramas/arquitectura.png)

La solución `TPI_Paseadores_Perros.sln` tiene 8 proyectos organizados en capas. Cada capa solo conoce a la
de abajo:

| Proyecto | Tipo | Responsabilidad |
|---|---|---|
| Domain.Model | Biblioteca de clases | Entidades del dominio con sus validaciones. No depende de nada. |
| Data | Biblioteca de clases | `PaseadoresContext` (EF Core), migraciones, repositorios (uno por entidad, con interfaz), datos de prueba y `ReporteRepository` (ADO.NET). |
| DTOs | Biblioteca de clases | Objetos que viajan entre la API y los clientes. No dependen del dominio. |
| Application.Services | Biblioteca de clases | Servicios con las reglas de negocio que necesitan consultar la base (email único, superposición de paseos, paseo ya liquidado, etc.), mapeo entidad ↔ DTO, hash de contraseñas y generación del token. |
| WebAPI | ASP.NET Core (Minimal API) | Endpoints REST agrupados por entidad, autenticación JWT, políticas por rol, Swagger y CORS. Registra las dependencias y al arrancar migra la base. |
| API.Clients | Biblioteca de clases | Un cliente HTTP por entidad, que heredan de `BaseApiClient` y traducen los errores de la API. Lo comparten escritorio y web. |
| Paseadores.WinForms | Windows Forms | Aplicación de escritorio. Gráfico con ScottPlot. |
| Paseadores.Blazor | Blazor WebAssembly | Aplicación web. Guarda el token en `localStorage` y lo agrega a cada pedido. Gráfico con Chart.js. |

**Flujo de un pedido.** La pantalla (escritorio o web) usa un cliente de `API.Clients`, que manda el DTO
por HTTPS con el token en el encabezado `Authorization: Bearer`. La API valida el token y el rol y llama al
servicio. El servicio aplica las reglas de negocio, arma la entidad del dominio (que valida sus datos) y la
guarda a través del repositorio. Los errores de validación se devuelven como `400 Bad Request` con el
mensaje, y la pantalla lo muestra.

## 6.1 Diferencias con la arquitectura propuesta

En la *Definición de Arquitectura* (charla con la IA, que se adjunta sin cambios) elegimos N-Capas. La
estructura se respetó, pero en la implementación nos apartamos de algunas recomendaciones:

| Recomendación de la IA | Qué hicimos | Por qué |
|---|---|---|
| SQLite para desarrollar | **SQL Server** | Es el motor que usamos en la materia y el que pide la consigna para los reportes con ADO.NET (`Microsoft.Data.SqlClient`). Con migraciones y la creación automática de la base, levantarlo en cada máquina no fue un problema. |
| FluentValidation en la capa de negocio | **Validaciones en las entidades** (`Domain.Model`) y en los servicios | Cada entidad valida en sus métodos `Set…` y no puede quedar inválida, se use desde donde se use. Las reglas que necesitan la base quedaron en los servicios. Evitamos sumar otra librería. |
| Controllers en la API | **Minimal API** | Los endpoints quedan agrupados por entidad en clases estáticas (`PaseoEndpoints`, etc.) con menos código repetido, y cada uno declara su política de autorización. |
| 3 proyectos (Data, Business, API) | **8 proyectos** | Se sumaron `Domain.Model` (entidades separadas del acceso a datos), `DTOs` (contratos que comparten la API y los clientes, en vez de estar en Business), `API.Clients` (cliente HTTP común a escritorio y web), y las dos interfaces: `Paseadores.WinForms` y `Paseadores.Blazor`. La capa Business se llama `Application.Services`. |

# 7. Tecnologías utilizadas

| Tecnología | Versión | Uso |
|---|---|---|
| Visual Studio 2022 | — | Entorno de desarrollo. |
| .NET / C# | .NET 8 | Plataforma de todos los proyectos (`net8.0`; escritorio `net8.0-windows`). |
| ASP.NET Core Minimal API | 8.0 | WebAPI. |
| Entity Framework Core (SqlServer, Tools) | 9.0.19 | ORM, migraciones y ABMC. |
| ADO.NET (`Microsoft.Data.SqlClient`) | — | Consultas de los reportes. |
| SQL Server | — | Base de datos. |
| Windows Forms | .NET 8 | Aplicación de escritorio. |
| Blazor WebAssembly | 8.0.25 | Aplicación web. |
| Microsoft.AspNetCore.Authentication.JwtBearer | 8.0.11 | Validación del token en la API. |
| System.IdentityModel.Tokens.Jwt | 8.23.0 | Generación del token. |
| BCrypt.Net-Next | 4.2.0 | Hash de contraseñas. |
| Swashbuckle.AspNetCore (Swagger) | 6.6.2 | Documentación y prueba de la API. |
| ScottPlot.WinForms | 5.1.59 | Gráfico del reporte en escritorio. |
| Chart.js | 4.4.4 | Gráfico del reporte en web (incluido en `wwwroot/lib`). |
| Microsoft.Extensions.Configuration.Json | 10.0.12 | Lectura de `appsettings.json` en escritorio. |
| Git y GitHub | — | Control de versiones. |
| Mermaid | — | Diagramas de este documento. |

# 8. Repositorio

<https://github.com/LautaroGiunta/tp-net-paseadores-mascotas>

En el `README.md` están los usuarios de prueba y los pasos para levantar la API, el escritorio y la web.
En la carpeta `docs/` del repositorio están este documento, los diagramas (fuente Mermaid e imagen) y los
documentos originales (*Informe preliminar* y *Definición de arquitectura*).
