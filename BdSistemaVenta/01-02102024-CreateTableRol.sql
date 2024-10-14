IF EXISTS (SELECT 1 FROM sys.tables WHERE name like 'Rol')
BEGIN
	DROP TABLE [dbo].[Rol]
END 
GO

CREATE TABLE [dbo].[ROL] (
	IdRol INT IDENTITY(1,1),
	Nombre VARCHAR(50),
	FechaRegistro DATETIME DEFAULT GETDATE(),
	CONSTRAINT pk_Rol PRIMARY KEY (IdRol)
);