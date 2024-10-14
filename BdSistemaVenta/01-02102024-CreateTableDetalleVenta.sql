IF EXISTS (SELECT 1 FROM sys.tables WHERE name like 'DetalleVenta')
BEGIN
	DROP TABLE [dbo].[DetalleVenta]
END 
GO

CREATE TABLE [dbo].[DetalleVenta] (
	IdDetalleVenta INT IDENTITY(1,1),
	IdVenta INT,
	IdProducto INT,
	Cantidad INT,
	Precio DECIMAL(10,2),
	Total DECIMAL(10,2),
	CONSTRAINT pk_DetalleVenta PRIMARY KEY (IdDetalleVenta),
	CONSTRAINT fk_DetalleVenta_Venta FOREIGN KEY (IdVenta) REFERENCES [dbo].[Venta] (IdVenta),
	CONSTRAINT fk_DetalleVenta_Producto FOREIGN KEY (IdProducto) REFERENCES [dbo].[Producto] (IdProducto)
);
