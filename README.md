# tp-net-paseadores-mascotas  
Comision 3EK02  
Integrantes:  
  Dentesano Valentino, legajo:54915, email: dentesanovalentino@gmail.com  
  Giunta Lautaro, legajo: 54714, email: lautogiunta@gmail.com  
  Taborda Fausto, legajo 54914, email: fausto.taborda05@gmail.com  

Carpeta Compartida de Google Drive con informacion sobre el negocio y la arquitectura:  
https://drive.google.com/drive/folders/1Go082EwDCfAqC32if1K6gshYj6In-kz1

## Documentacion de la entrega final

Todo esta en la carpeta `docs/`:

| Ruta | Contenido |
|------|-----------|
| `docs/Documentacion_TPI.docx` | Documento de la entrega: caratula, descripcion, requerimientos, capturas, modelo de datos, modelo de clases, arquitectura implementada y tecnologias. |
| `docs/Documentacion_TPI.md` | Fuente del documento (el `.docx` se genera a partir de este archivo). |
| `docs/diagramas/` | Modelo de clases, modelo de datos y arquitectura, en Mermaid (`.mmd`) y como imagen (`.png`). |
| `docs/capturas/` | Capturas de escritorio (`esc-*`) y web (`web-*`) con los tres roles, y de los reportes. |
| `docs/originales/` | Documentos de agosto: informe preliminar y definicion de arquitectura (el chat con la IA). |

El modelo de dominio preliminar quedo solo en el Drive: esta desactualizado y lo reemplaza el
modelo de clases de `docs/diagramas/`.

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

Los parametros del token (emisor, audiencia y duracion) estan en la seccion `Jwt` del
`appsettings.json` de la WebAPI. El token dura 120 minutos.

### Clave para firmar los tokens (una vez por maquina)

La clave con la que la API firma los tokens no esta en el repositorio: en `appsettings.json`,
`Jwt:ClaveSecreta` esta vacia y cada uno la carga en sus *user-secrets*, que se guardan fuera de
la carpeta del proyecto. Sin la clave, o si tiene menos de 32 caracteres, la API no arranca y
avisa que falta.

Desde la carpeta `TPI_Paseadores_Perros`, en PowerShell (arma una clave al azar de 72 caracteres):

```
dotnet user-secrets set "Jwt:ClaveSecreta" "$(New-Guid)$(New-Guid)" --project WebAPI
```

Tambien se puede desde Visual Studio: clic derecho en WebAPI > *Administrar secretos de usuario*,
y en el archivo que se abre poner `{ "Jwt": { "ClaveSecreta": "..." } }` con una clave propia de
32 caracteres o mas. Cada maquina puede tener una clave distinta, porque el token lo firma y lo
valida la misma API.

Fuera de Development no se leen los user-secrets: ahi la clave va en la variable de entorno
`Jwt__ClaveSecreta`.

### Permisos por rol

| Operacion                            | Admin | Dueno                        | Paseador            |
|--------------------------------------|-------|------------------------------|---------------------|
| Consultar duenos                     | todos | solo el mismo                | no                  |
| Consultar perros                     | todos | solo los suyos               | todos, sin el dueno |
| Consultar paseadores                 | todos | todos, sin email ni telefono | solo el mismo       |
| Buscar paseadores por texto          | si    | no                           | no                  |
| Consultar paseos                     | todos | los de sus perros            | los suyos           |
| Alta/baja/modificacion de Duenos     | si    | no                           | no                  |
| Alta/baja/modificacion de Paseadores | si    | no                           | no                  |
| Alta/baja/modificacion de Perros     | si    | los suyos                    | no                  |
| Alta/baja/modificacion de Paseos     | si    | los de sus perros            | los suyos           |
| Administradores (todo, incluso GET)  | si    | no                           | no                  |
| Liquidaciones (todo, incluso GET)    | si    | no                           | no                  |
| Reportes                             | si    | no                           | no                  |

Las consultas las filtra la API con el usuario del token: si un dueno o un paseador pide datos
ajenos (por id o con otro filtro en `/paseos/buscar`), recibe solo lo suyo o un 403. Hay dos
excepciones a proposito: el dueno ve la lista de paseadores, porque la necesita para elegir uno
al pedir un paseo (pero sin email ni telefono), y el paseador ve la lista de perros, porque la
necesita para registrar un paseo (pero sin el nombre del dueno).

El menu de la pantalla principal de escritorio esconde lo mismo que la API bloquea.

### Como probar desde Swagger

1. Ejecutar `POST /api/auth/login` con alguno de los usuarios de prueba.
2. Copiar el valor de `token` de la respuesta.
3. Tocar el boton **Authorize** (arriba a la derecha) y pegar solo el token, sin escribir
   "Bearer" adelante.
4. A partir de ahi Swagger manda el encabezado en cada llamada.

### Como correr la aplicacion de escritorio

La app de escritorio lee la URL de la API de `Paseadores.WinForms/appsettings.json` (clave
`UrlApi`, por defecto `https://localhost:7140`, el perfil **https** de la WebAPI). Hay que
levantar la WebAPI antes de abrirla; si se cambia el puerto, se cambia en ese archivo, sin
recompilar.

### Como correr la aplicacion web (Blazor)

`Paseadores.Blazor` es Blazor WebAssembly: corre en el navegador y le pega a la misma WebAPI
que el escritorio, con el mismo token JWT (lo guarda en `localStorage`).

1. Levantar la WebAPI con el perfil **https**. Blazor toma la URL de la API de
   `Paseadores.Blazor/wwwroot/appsettings.json` (clave `UrlApi`, por defecto `https://localhost:7140`).
2. Levantar `Paseadores.Blazor` con cualquiera de sus perfiles: `https://localhost:7202` o
   `http://localhost:5297`. Los dos estan en `Cors:OrigenesPermitidos` del `appsettings.json` de la
   WebAPI; si Blazor corre en otro puerto, hay que agregarlo ahi.
3. Entrar con alguno de los usuarios de prueba. El menu se recorta segun el rol igual que en
   escritorio, y las paginas solo-Admin (duenos, paseadores, administradores, liquidaciones y
   reportes) redirigen al inicio si se entra por URL directa con otro rol. La de perros redirige
   al paseador.

Desde la terminal, en dos consolas (antes, cargar la clave de los tokens si todavia no se hizo;
ver *Clave para firmar los tokens*):

```
dotnet run --project TPI_Paseadores_Perros/WebAPI --launch-profile https
dotnet run --project TPI_Paseadores_Perros/Paseadores.Blazor --launch-profile https
```

En Visual Studio: clic derecho en la solucion > Configurar proyectos de inicio > Varios proyectos
de inicio, con WebAPI y Paseadores.Blazor en "Iniciar" (perfil https en los dos).
