IF EXISTS (SELECT 1 FROM sys.tables WHERE name like 'Categoria')
BEGIN
	DROP TABLE [dbo].[Categoria]
END 
GO

CREATE TABLE [dbo].[Categoria] (
	IdCategoria INT IDENTITY(1,1),
	Nombre VARCHAR(50),
	EsActivo BIT DEFAULT 1,
	FechaRegistro DATETIME DEFAULT GETDATE(),
	CONSTRAINT pk_Categoria PRIMARY KEY (IdCategoria)
);
