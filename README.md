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
