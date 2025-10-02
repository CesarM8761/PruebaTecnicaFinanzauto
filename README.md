# Blog Finanzauto - Prueba Técnica Full-Stack

## Descripción

Aplicación web tipo Blog desarrollada como prueba técnica, que permite a los usuarios registrarse, autenticarse y gestionar publicaciones (crear, listar, editar y eliminar). Cada publicación incluye título, contenido, fecha de creación y autor.

## Arquitectura del Proyecto

```
PruebaTecnicaFinanzauto/
├── Backend/
│   ├── BlogTecnica/          # API REST en .NET
│   └── Datos/                # Capa de acceso a datos también en .Net
├── Frontend/
│   └── blog-prueba/          # Aplicación web React
├── Docker/
│   ├── Backend/              # API compilada para correr en docker
│   ├── Frontend/             # Dockerfile para React y carpeta Build generada con npm, monta el servicio con Nginx
│   ├── db-init/              # Scripts de inicialización de BD
│   └── docker-compose.yml    # Docker compose
└── Data/                     # Script de la base de datos exportado desde sql server
```

## Tecnologías Utilizadas

### Backend
- **.NET Core** (C# con ASP.NET)
- **Entity Framework** 
- **SQL Server** como base de datos
- **JWT** para autenticación


### Frontend
- **React** 

### Endpoints del API

### Usuarios
- `GET /api/Usuarios` - Listar todos los usuarios
- `POST /api/Usuarios` - Crear nuevo usuario (registro)
- `GET /api/Usuarios/{id}` - Obtener un usuario específico
- `PUT /api/Usuarios/{id}` - Actualizar datos de usuario
- `DELETE /api/Usuarios/{id}` - Eliminar usuario
- `POST /api/Usuarios/auth` - Autenticar usuario (login)

### Publicaciones
- `GET /api/Publicaciones` - Listar todas las publicaciones
- `POST /api/Publicaciones` - Crear nueva publicación
- `GET /api/Publicaciones/{id}` - Obtener una publicación específica
- `PUT /api/Publicaciones/{id}` - Actualizar publicación existente
- `DELETE /api/Publicaciones/{id}` - Eliminar publicación
- `GET /api/Publicaciones/Usuario/{idUsuario}` - Listar publicaciones de un usuario específico

### Categorías
- `GET /api/Categorias` - Listar todas las categorías disponibles
- `GET /api/Categorias/{id}` - Obtener una categoría específica

### Imágenes
- `GET /api/Imagenes` - Listar todas las imágenes
- `POST /api/Imagenes` - Subir nueva imagen
- `GET /api/Imagenes/{id}` - Obtener una imagen específica

## Cómo ejecutar

1. **Clonar el repositorio**
```bash
git clone https://github.com/tu-usuario/PruebaTecnicaFinanzauto.git
cd PruebaTecnicaFinanzauto
```

### Opción 1: Ejecución con Docker 
1. Ir a la carpeta PruebaTecnicaFinanzauto/Docker
2. **Levantar los servicios con Docker Compose**
```bash
cd Docker
docker-compose up --build
```
3. **Acceder a la aplicación**
- Frontend: http://localhost:3000
- Backend API: http://localhost:5000
- SQL Server: localhost:1433

4. **Detener los servicios**
```bash
docker-compose down
```

### 💻 Opción 2: Visualizar Servicios montados en nube

En caso de que por algún motivo no funcione el docker-compose, monté los servicios en la nube para una correcta visualización.

API: http://www.pruebatecnicafz.somee.com/api/Publicaciones
API Documentación:  http://www.pruebatecnicafz.somee.com/swagger/index.html

Frontend: https://cesarm8761.github.io/PruebaTecnicaFront/#/

## Usuarios de Prueba

Generé 2 usuarios de prueba, uno con el que se realizaron las publicaciones y otro solamente de visualización, sin embargo se pueden crear usuarios nuevos desde la pagina de registro

| Usuario | Contraseña | 
|---------|------------|
| prueba@gmail.com | prueba | 
| publicador@gmail.com | publicador | 


## Funcionalidades Implementadas

### Autenticación y Autorización
- Registro de usuarios con validación
- Login con JWT
- Protección de rutas privadas

### Gestión de Publicaciones
- Crear publicaciones (usuarios autenticados)
- Listar todas las publicaciones
- Ver detalle de publicación
- Editar publicaciones propias
- Eliminar publicaciones propias
- Fecha de creación automática
- Asociación autor-publicación

### Interfaz de Usuario
- Diseño responsive
- Página de inicio (Landing)
- Formularios de registro e inicio de sesión
- Lista de publicaciones
- Formulario de creación/edición
- Panel de usuario
- Página 404 

### Docker
-  Docker Compose para orquestación
-  Scripts de inicialización de BD

## Estructura de la Base de Datos

### Tabla: dbo.Usuarios
| Campo | Tipo | Restricción | Descripción |
|-------|------|-------------|-------------|
| idUsuario | int | PK, Not Null | Identificador único del usuario |
| Nombre | nvarchar(100) | Not Null | Nombre completo del usuario |
| Username | nvarchar(50) | Not Null | Nombre de usuario para login |
| email | nvarchar(150) | Not Null | Correo electrónico |
| password | nvarchar(200) | Not Null | Contraseña hasheada |

### Tabla: dbo.Categorias
| Campo | Tipo | Restricción | Descripción |
|-------|------|-------------|-------------|
| idCategoria | int | PK, Not Null | Identificador único de categoría |
| nombreCategoria | nvarchar(100) | Not Null | Nombre de la categoría |

### Tabla: dbo.Imagenes
| Campo | Tipo | Restricción | Descripción |
|-------|------|-------------|-------------|
| idImagen | int | PK, FK, Not Null | Identificador único de imagen |
| Imagen | varbinary(max) | Null | Datos binarios de la imagen |

### Tabla: dbo.Publicaciones
| Campo | Tipo | Restricción | Descripción |
|-------|------|-------------|-------------|
| idPublicacion | int | PK, Not Null | Identificador único de publicación |
| Titulo | nvarchar(200) | Not Null | Título de la publicación |
| Contenido | nvarchar(max) | Not Null | Contenido completo de la publicación |
| FechaCreacion | datetime | Not Null | Fecha de creación automática |
| FechaModificacion | datetime | Null | Fecha de última modificación |
| idAutor | int | FK, Not Null | Referencia al usuario autor |
| idImagenPublicacion | int | FK, Null | Referencia a imagen asociada |
| idCategoria | int | FK, Not Null | Referencia a la categoría |

### Tabla: dbo.PubliImagen
| Campo | Tipo | Restricción | Descripción |
|-------|------|-------------|-------------|
| idImagen | int | PK, FK, Not Null | Referencia a la imagen |
| idPublicacion | int | PK, FK, Not Null | Referencia a la publicación |
| Posicion | int | Not Null | Orden de la imagen en la publicación |

### Relaciones
- **Publicaciones → Usuarios**: `idAutor` referencia a `idUsuario`
- **Publicaciones → Categorias**: `idCategoria` referencia a `idCategoria`
- **Publicaciones → Imagenes**: `idImagenPublicacion` referencia a `idImagen`
- **PubliImagen → Imagenes**: `idImagen` referencia a `idImagen`
- **PubliImagen → Publicaciones**: `idPublicacion` referencia a `idPublicacion`

## Solución de Problemas

### Error al levantar Docker
- Asegurar que los puertos 3000, 5000 y 1433 no estén en uso
- Verificar que Docker Desktop esté ejecutándose

## 👨‍💻 Autor

Desarrollado por Cesar Mauricio Martinez Navarro como prueba técnica para Finanzauto.

---

**Fecha de entrega:** 2 de Octubre de 2025  
