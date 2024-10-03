# Refactor

- Crear las clases estáticas **AlumnoEndpoints** y **CursoEndpoints**
- Crear los métodos estáticas **MapAlumnoEndpoints** y **MapCursoEndpoints**
- Mover los endpoints relacionados del archivo **Program.cs** a sus respectivas clases.
- Agregar las referencias de las clases **AlumnoEndpoints** y **CursoEndpoints** a la clase **Program**

```csharp
app.MapGroup("/api")
    .MapAlgoEndpoints()
    .WithTags("Algo");
```

- Habilitar en Swagger que el botón **Try out** siempre se encuentre activado.

# Conexión con Postgres mediante Scaffold

## Instalar Entity Framework Core CLI
Para poder ejecutar los comandos de **Entity Framework Core** es necesario instalar su CLI (**Command Line Interface**), lo cual se realiza una única vez con el siguiente comando:
```
dotnet tool install --global dotnet-ef
```

Si ya se instaló una vez se debe actualizarlo con el siguiente comando:
```
dotnet tool update --global dotnet-ef
```

Para verificar si se encuentra instalado correctamente se puede usar el comando:
```
dotnet-ef
```

## Nuget

- Instalar los siguientes paquetes nuget en la versión mas reciente:
    - Microsoft.EntityFrameworkCore
    - Microsoft.EntityFrameworkCore.Design
    - Microsoft.EntityFrameworkCore.Tools
    - Npgsql.EntityFrameworkCore.PostgreSQL

Ejercutar los siguientes comandos (desde el directorio de los proyectos):
```
dotnet add package Microsoft.EntityFrameworkCore
```

```
dotnet add package Microsoft.EntityFrameworkCore.Design
```

```
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

```
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

Para verificar si se instalaron correctamente revisar el contenido del archivo **Api.csproj**

## Connection string

La **cadena de conexión** o **connection string** son las credenciales necesarias para conectarse a cualquier tipo de repositorio de datos, por ejemplo una base de datos relacional como Postgres o MySql

Para configurar el **ConnectionString** se debe editar el archivo **appsettings.json** y agregar el siguiente apartado:

```json
"ConnectionStrings": {
    "ITEM_DB" : "Server=IP_SERVIDOR;Database=NOMBRE_BD;Username=USUARIO;Password=CONTRASEÑA"
  }
```

La configuración se debe completar con los datos correspondientes de la computadora en donde se encuentre la base de datos:

```json
"ConnectionStrings": {
    "escuela_db" : "Server=localhost;Database=escuela;Username=administradorroot;Password=tecnica12"
  }
```

El archivo **appsettings.json** queda:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "escuela_db" : "Server=localhost;Database=escuela;Username=administrador;Password=tecnica12"
  }
}
```

## Scaffold

Para realizar el proceso de scaffold desde una base de datos existente se debe ejecutar un comando con la forma de:

```
dotnet ef dbcontext scaffold "CONNECTION_STRING" DRIVER_CONEXION
```

Para el caso puntual de **Postgres** se debe explicitar su cadena de conexión y su driver asociado, con el siguiente comando:
```
dotnet ef dbcontext scaffold "Server=192.168.5.100;Database=escuela;Username=administrador;Password=tecnica12" Npgsql.EntityFrameworkCore.PostgreSQL -o Models
```

Antes de ejecutar el comando se debe tener en cuenta que el proyecto debe compilar (no debe tener error alguno).

Si el proceso fue existoso, se debe crear un directorio con el nombre de **Models** dentro de nuestro proyecto.

## Refactor - Post Scaffold

- Elimnar las clases duplicadas
- Remover el **ConnectionString** de la clase **Context** (por seguridad NUNCA debe debe quedar credencias de ningun servidor en código fuente)
