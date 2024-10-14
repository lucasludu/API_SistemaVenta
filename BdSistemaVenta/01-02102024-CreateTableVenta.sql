IF EXISTS (SELECT 1 FROM sys.tables WHERE name like 'Venta')
BEGIN
	DROP TABLE [dbo].[Venta]
END 
GO

CREATE TABLE [dbo].[Venta] (
	IdVenta INT IDENTITY(1,1),
	NumeroDocumento VARCHAR(50),
	TipoPago VARCHAR(50),
	Total DECIMAL(10,2),
	FechaRegistro DATETIME DEFAULT GETDATE(),
	CONSTRAINT pk_Venta PRIMARY KEY (IdVenta)
);
