# 🛠️ Proyecto Full Stack con Autenticación JWT via Cookies

## 📌 Descripción
Este proyecto es una aplicación full stack que incluye:
- **Backend en .NET** con dos microservicios: `AuthService` y `PostService`.
- **Base de datos en SQL Server**, gestionada en contenedores con `Docker Compose`.
- **Frontend en React (TypeScript - TSX)**, que consume los servicios backend y maneja la autenticación vía JWT almacenado en cookies seguras.

## 🏗️ Arquitectura

### 📌 Backend (ASP.NET Core)
El backend está compuesto por **dos microservicios**:
1. **AuthService**: Maneja autenticación y generación de tokens JWT.
2. **PostService**: Gestiona las publicaciones de los usuarios.

**⚡ Comunicación JWT por Cookies**
- El `AuthService` genera un **JWT** y lo almacena en una cookie **HttpOnly**.
- Los microservicios validan la cookie en cada petición para autenticar al usuario.

### 🎨 Frontend (React + TSX)
El frontend es una SPA creada con **React + TypeScript**, que:
- Consume los endpoints de autenticación y publicaciones.
- Maneja la sesión del usuario con **contextos y cookies**.
- Protege rutas con `ProtectedRoute` y `useContext`.

## 🚀 Tecnologías Utilizadas
### Backend:
- ASP.NET Core
- Entity Framework Core
- SQL Server
- JWT Authentication con Cookies
- Docker & Docker Compose

### Frontend:
- React con TypeScript (TSX)
- React Router
- React Context API
- Fetch API con autenticación por cookies

## 🏗️ Configuración del Proyecto

### 1️⃣ Clonar el repositorio
```sh
    git clone https://github.com/tu-repo/proyecto-fullstack.git
    cd proyecto-fullstack
```

### 2️⃣ Configurar las variables de entorno
Crea un archivo `.env` con las configuraciones necesarias para conectar la base de datos y los servicios.

### 3️⃣ Configurar y levantar los contenedores con Docker
> **Asegúrate de tener `Docker` y `Docker Compose` instalados.**

```sh
    docker-compose up --build
```
Esto iniciará:
- **SQL Server** con dos bases de datos: `usuarios_db` y `publicaciones_db`.
- **AuthService** y **PostService** en puertos diferentes.
- **Frontend React** corriendo en el puerto 5173.

### 4️⃣ Acceder a la aplicación
- **Frontend**: [http://localhost:5173](http://localhost:5173)
- **AuthService**: [http://localhost:44342](http://localhost:44342)
- **PostService**: [http://localhost:44344](http://localhost:44344)

## 📡 Endpoints Importantes

### 🔐 Autenticación (`AuthService`)
- **POST `/api/Auth/login`** → Inicia sesión y almacena el JWT en cookies.
- **GET `/api/Auth/me`** → Retorna el usuario autenticado.
- **POST `/api/Auth/logout`** → Cierra sesión eliminando la cookie JWT.

### 📝 Publicaciones (`PostService`)
- **GET `/api/Publicaciones`** → Obtiene todas las publicaciones.
- **POST `/api/Publicaciones`** → Crea una publicación (requiere autenticación).

## 🔥 Extras
### 📦 Migraciones de Base de Datos
Si es necesario ejecutar migraciones en **Entity Framework Core**:
```sh
    dotnet ef migrations add InitialCreate --project AuthService
    dotnet ef database update --project AuthService
```
### 📌 Notas
- **El token JWT se almacena en cookies seguras HttpOnly**, por lo que solo es accesible desde el backend.
- **Los microservicios validan la autenticación leyendo la cookie JWT** en cada petición.
- **El frontend en React maneja la sesión con `useContext` y obtiene el usuario autenticado desde `/api/Auth/me`.**

---

💡 **Listo para desplegar tu proyecto!** 🚀

