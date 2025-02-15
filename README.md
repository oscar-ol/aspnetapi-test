# ASP.NET API (DEV TEST)

## Requisitos Previos

1. **Instalar .NET SDK Core 8.0**:  
   Si aún no tienes instalado .NET SDK 8.0, puedes descargarlo desde [aquí](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).

## Ejecución del Proyecto

- Para ejecutar el proyecto, utiliza el siguiente comando:

    ```bash
    dotnet run
    ```

- El proyecto estará disponible en el siguiente enlace:

    [http://localhost:5033](http://localhost:5033)

## APIs Disponibles

- **Crear usuario (POST):**  
  Realiza una solicitud `POST` a la siguiente URL para crear un nuevo usuario. El cuerpo de la solicitud debe ser un JSON con los datos del usuario:

    [http://localhost:5033/api/usuarios](http://localhost:5033/api/usuarios)

    **Ejemplo de solicitud:**

    ```json
    {
        "Nombre": "Juan Perez",
        "Email": "juan.perez@example.com",
        "Direccion": "Calle 123"
    }
    ```

- **Listar usuarios (GET):**  
  Realiza una solicitud `GET` a la siguiente URL para obtener todos los usuarios:

    [http://localhost:5033/api/usuarios](http://localhost:5033/api/usuarios)

## Migraciones de Base de Datos

### 1. Crear una nueva entidad  
Crea una nueva clase de entidad, por ejemplo, `Proyecto.cs`.

```csharp
public class Proyecto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    // Otros atributos...
}
```

### 2. Agregar la entidad al contexto de base de datos  
Luego, agrega la entidad al contexto de la base de datos en el archivo `AppDbContext.cs`:

```csharp
public DbSet<Proyecto> Proyectos { get; set; }
```

### 3. Crear una migración  
Después de haber agregado la entidad al `DbContext`, crea la migración usando el siguiente comando:

```bash
dotnet ef migrations add CrearTablaProyectos
```

Luego, actualiza la base de datos con la migración recién creada:

```bash
dotnet ef database update
```

### 4. Eliminar migraciones no aplicadas  
Si necesitas eliminar una migración que aún no se ha aplicado a la base de datos:

```bash
dotnet ef migrations remove
```

### 5. Eliminar la base de datos  
Si deseas eliminar la base de datos completamente (no la vuelve a crear):

```bash
dotnet ef database drop
```

### 6. Añadir un campo a una tabla existente  
Para agregar una nueva columna a una tabla existente (por ejemplo, añadiendo un campo `Email`):

1. Agrega el nuevo campo a tu entidad, por ejemplo, en `Usuario.cs`:

    ```csharp
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; } // Nuevo campo
    }
    ```

2. Crea una nueva migración y actualiza la base de datos:

    ```bash
    dotnet ef migrations add AgregarEmailAUsuario
    dotnet ef database update
    ```

### 7. Listar migraciones aplicadas  
Para ver todas las migraciones aplicadas en tu proyecto:

```bash
dotnet ef migrations list
```

### 8. Volver a una migración específica  
Si necesitas volver a una migración específica, puedes hacerlo utilizando el **timestamp**_NombreDeLaMigracion:

```bash
dotnet ef database update 20250215104215_AgregarEmailAUsuario
```

> **Nota:** El **timestamp** corresponde al prefijo de la migración generado automáticamente al crearla.
