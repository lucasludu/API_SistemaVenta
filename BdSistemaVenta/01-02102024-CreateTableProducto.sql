IF EXISTS (SELECT 1 FROM sys.tables WHERE name like 'Producto')
BEGIN
	DROP TABLE [dbo].[Producto]
END 
GO

CREATE TABLE [dbo].[Producto] (
	IdProducto INT IDENTITY(1,1),
	Nombre VARCHAR(100),
	IdCategoria INT,
	Stock INT,
	Precio DECIMAL(10,2),
	EsActivo BIT DEFAULT 1,
	FechaRegistro DATETIME DEFAULT GETDATE(),
	CONSTRAINT pk_Producto PRIMARY KEY (IdProducto),
	CONSTRAINT fk_Producto_Categoria FOREIGN KEY (IdCategoria) REFERENCES [dbo].[Categoria] (IdCategoria)
);
