# tp-net-paseadores-mascotas  
Comision 3EK02  
Integrantes:  
  Dentesano Valentino, legajo:54915, email: dentesanovalentino@gmail.com  
  Giunta Lautaro, legajo: 54714, email: lautogiunta@gmail.com  
  Taborda Fausto, legajo 54914, email: fausto.taborda05@gmail.com  

Carpeta Compartida de Google Drive con informacion sobre el negocio y la arquitectura:  
https://drive.google.com/drive/folders/1Go082EwDCfAqC32if1K6gshYj6In-kz1

## Usuarios de prueba

La base de datos se crea sola al arrancar la WebAPI (Database.Migrate()) e incluye estos
usuarios, sembrados por la migracion SeedUsuariosIniciales:

| Rol      | Email                  | Contrasena |
|----------|------------------------|------------|
| Admin    | admin@paseos.com       | Admin123   |
| Dueno    | ana.gomez@paseos.com   | Dueno123   |
| Paseador | carlos.ruiz@paseos.com | Paseo123   |

El menu de la pantalla principal se habilita segun el rol del usuario que inicia sesion.

### Datos de prueba

Al arrancar, la WebAPI tambien carga datos de prueba (`Data/DatosDePrueba.cs`), **solo si la
base no tiene perros ni paseos** (asi no se mezclan con datos cargados a mano). Para
regenerarlos desde cero, borrar la base `PaseadoresDB` y volver a levantar la API.

- 3 duenos mas (`martin.lopez`, `lucia.fernandez`, `jorge.perez` `@paseos.com`, contrasena `Dueno123`)
- 2 paseadores mas (`sofia.martinez`, `diego.suarez` `@paseos.com`, contrasena `Paseo123`)
- 7 perros repartidos entre los duenos
- Paseos de los ultimos 3 meses y de las proximas 2 semanas (las fechas son relativas al dia
  en que se carga, asi siempre hay paseos hechos y agendados)
- Liquidaciones de los meses anteriores al mes pasado; el mes pasado y el actual quedan
  pendientes para poder mostrar el alta de una liquidacion

## Liquidaciones a paseadores (maestro/detalle)

Una liquidacion es lo que se le paga a un paseador por los paseos de un periodo. La cabecera
(maestro) tiene el paseador y las fechas desde/hasta; el detalle son los paseos que se le pagan,
cada uno con el importe congelado al precio que tenia el paseo al liquidarlo. El total es la suma
de los importes. Cabecera y detalle viajan juntos y se guardan en una sola operacion
(`POST /liquidaciones` o `PUT /liquidaciones`).

Reglas de negocio (las valida el dominio y la API; las pantallas solo avisan antes de mandar):

- Solo entran paseos del paseador elegido, dentro del periodo, **ya terminados** y que **no esten
  en otra liquidacion**: un paseo se le paga una sola vez. `GET /liquidaciones/paseos-pendientes`
  devuelve justamente esos.
- Una liquidacion tiene que incluir al menos un paseo.
- Un paseo que ya fue liquidado no se puede modificar ni eliminar. Si se elimina la liquidacion,
  sus paseos vuelven a quedar pendientes.
- Solo el Admin puede verlas y operarlas.

Como se usa (escritorio: menu *Gestion > Liquidaciones*; web: *Liquidaciones* en el menu, ruta
`/liquidaciones`):

1. Elegir el paseador y el periodo (por defecto viene el mes pasado completo) y buscar los paseos
   pendientes.
2. Pasar los paseos de la grilla de pendientes al detalle (y volverlos a sacar si hace falta).
   El total se recalcula solo.
3. Guardar.

Para editar una existente, en escritorio se hace doble clic en la grilla de arriba y en web se usa
el boton *Editar*: se cargan sus lineas y ademas los paseos que todavia se le podrian sumar.

## Reportes

Los dos reportes se consultan con **ADO.NET** (`Data/ReporteRepository.cs`: `SqlConnection`,
`SqlCommand` con parametros y `SqlDataReader`) y estan en escritorio (menu *Reportes*) y en web.
Solo los ve el Admin.

| Reporte | Contenido |
|---------|-----------|
| Recaudacion mensual por paseador | Grafico de barras agrupadas por mes + tabla. En escritorio usa ScottPlot; en web, Chart.js (incluido en `wwwroot/lib`, no necesita internet). |
| Actividad por perro | Tabla con paseos, minutos, gasto y ultimo paseo de cada perro en el periodo (incluye los que no salieron). |

## Seguridad (autenticacion con token)

El login (`POST /api/auth/login`) devuelve un token JWT junto con los datos del usuario:

```json
{ "token": "eyJhbGciOi...", "usuario": { "id": 1, "nombre": "Admin", "rol": "Admin" } }
```

Ese token hay que mandarlo en el encabezado `Authorization: Bearer <token>` en todas las
demas llamadas. El unico endpoint abierto es el login; el resto responde 401 sin token y
403 cuando el rol no alcanza.

Los parametros del token (clave de firma, emisor, audiencia y duracion) estan en la
seccion `Jwt` del `appsettings.json` de la WebAPI. El token dura 120 minutos.

### Permisos por rol

| Operacion                           | Admin | Dueno | Paseador |
|-------------------------------------|-------|-------|----------|
| Consultar (GET) cualquier entidad   | si    | si    | si       |
| Alta/baja/modificacion de Duenos    | si    | no    | no       |
| Alta/baja/modificacion de Paseadores| si    | no    | no       |
| Alta/baja/modificacion de Perros    | si    | si    | no       |
| Alta/baja/modificacion de Paseos    | si    | si    | si       |
| Administradores (todo, incluso GET) | si    | no    | no       |
| Liquidaciones (todo, incluso GET)   | si    | no    | no       |
| Reportes                            | si    | no    | no       |

El menu de la pantalla principal de escritorio esconde lo mismo que la API bloquea.

### Como probar desde Swagger

1. Ejecutar `POST /api/auth/login` con alguno de los usuarios de prueba.
2. Copiar el valor de `token` de la respuesta.
3. Tocar el boton **Authorize** (arriba a la derecha) y pegar solo el token, sin escribir
   "Bearer" adelante.
4. A partir de ahi Swagger manda el encabezado en cada llamada.

### Como correr la aplicacion de escritorio

La app de escritorio apunta a `https://localhost:7140` (constante `ApiClient.UrlBase`).
Hay que levantar la WebAPI con el perfil **https** antes de abrirla; si se cambia el
puerto, se cambia esa unica constante.

### Como correr la aplicacion web (Blazor)

`Paseadores.Blazor` es Blazor WebAssembly: corre en el navegador y le pega a la misma WebAPI
que el escritorio, con el mismo token JWT (lo guarda en `localStorage`).

1. Levantar la WebAPI (cualquiera de los dos perfiles expone `http://localhost:5206`, que es la
   URL que usa Blazor en `Program.cs`).
2. Levantar `Paseadores.Blazor` con el perfil **https** (`https://localhost:7202`). Tiene que ser
   ese origen porque es el unico que permite la politica CORS `PermitirBlazor` de la WebAPI; si se
   cambia el puerto, hay que cambiarlo tambien ahi.
3. Entrar con alguno de los usuarios de prueba. El menu se recorta segun el rol igual que en
   escritorio, y las paginas solo-Admin (administradores, liquidaciones y reportes) redirigen al
   inicio si se entra por URL directa con otro rol.

Desde la terminal, en dos consolas:

```
dotnet run --project TPI_Paseadores_Perros/WebAPI --launch-profile https
dotnet run --project TPI_Paseadores_Perros/Paseadores.Blazor --launch-profile https
```

En Visual Studio: clic derecho en la solucion > Configurar proyectos de inicio > Varios proyectos
de inicio, con WebAPI y Paseadores.Blazor en "Iniciar" (perfil https en los dos).
