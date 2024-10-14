IF EXISTS (SELECT 1 FROM sys.tables WHERE name like 'Usuario')
BEGIN
	DROP TABLE [dbo].[Usuario]
END 
GO

CREATE TABLE [dbo].[Usuario] (
	IdUsuario INT IDENTITY(1,1),
	NombreCompleto VARCHAR(100),
	Correo VARCHAR(50),
	Clave VARCHAR(50),
	EsActivo BIT DEFAULT 1,
	FechaRegistro DATETIME DEFAULT GETDATE(),
	IdRol INT,
	CONSTRAINT pk_Usuario PRIMARY KEY (IdUsuario),
	CONSTRAINT fk_Usuario_Rol FOREIGN KEY (IdRol) REFERENCES [dbo].[Rol] (IdRol)
);
