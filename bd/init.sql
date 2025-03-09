
-- ==========================

USE master;
GO

-- Crear la base de datos de usuarios
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'usuarios_db')
BEGIN
    CREATE DATABASE usuarios_db;
END;
GO

-- Crear la base de datos de publicaciones
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'posts_db')
BEGIN
    CREATE DATABASE posts_db;
END;
GO

-- ==========================
USE master;
GO
CREATE LOGIN app_user WITH PASSWORD = 'SuperSecure123!';
GO


-- ==========================
USE usuarios_db;
GO

CREATE USER app_user FOR LOGIN app_user;
ALTER ROLE db_owner ADD MEMBER app_user;
GO

CREATE TABLE usuarios (
    usuario_id INT IDENTITY(1,1) PRIMARY KEY,
    correo VARCHAR(255) NOT NULL UNIQUE,
    contrasena_hash VARCHAR(255) NOT NULL,
    primer_nombre VARCHAR(100) NOT NULL,
    segundo_nombre VARCHAR(100),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    fecha_actualizacion DATETIME DEFAULT GETDATE(),
    activo BIT DEFAULT 1
);
GO

CREATE TABLE tokens (
    token_id INT IDENTITY(1,1) PRIMARY KEY,
    usuario_id INT NOT NULL,
    nuevo_token VARCHAR(255) NOT NULL,
    fecha_creacion DATETIME DEFAULT GETDATE(),
    expiracion DATETIME NOT NULL,
    FOREIGN KEY (usuario_id) REFERENCES usuarios(usuario_id) ON DELETE CASCADE
);
GO

-- ==========================
USE posts_db;
GO

CREATE USER app_user FOR LOGIN app_user;
ALTER ROLE db_owner ADD MEMBER app_user;
GO

CREATE TABLE publicaciones (
    publicacion_id INT IDENTITY(1,1) PRIMARY KEY,
    usuario_id INT NOT NULL,
    titulo VARCHAR(255) NOT NULL,
    contenido TEXT NOT NULL,
    fecha_creacion DATETIME DEFAULT GETDATE(),
    fecha_actualizacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (usuario_id) REFERENCES usuarios_db.dbo.usuarios(usuario_id) ON DELETE CASCADE
);
GO
