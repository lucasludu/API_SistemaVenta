IF EXISTS (SELECT 1 FROM sys.tables WHERE name like 'Menu')
BEGIN
	INSERT INTO [dbo].[Menu] (Nombre, Icono, Url)
	SELECT Nombre, Icono, Url
	FROM (
		VALUES
		('DashBoard','dashboard','/pages/dashboard'),
		('Usuarios','group','/pages/usuarios'),
		('Productos','collections_bookmark','/pages/productos'),
		('Venta','currency_exchange','/pages/venta'),
		('Historial Ventas','edit_note','/pages/historial_venta'),
		('Reportes','receipt','/pages/reportes')
	) AS NuevosMenu (Nombre, Icono, Url)
	WHERE NOT EXISTS (
		SELECT 1
		FROM [dbo].[Menu] m
		WHERE m.Nombre = NuevosMenu.Nombre
	);
END
