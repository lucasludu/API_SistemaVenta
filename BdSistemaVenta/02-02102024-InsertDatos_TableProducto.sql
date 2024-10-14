IF EXISTS (SELECT 1 FROM sys.tables WHERE name like 'Producto')
BEGIN
	INSERT INTO [dbo].[Producto] (Nombre, IdCategoria, Stock, Precio, EsActivo)
	SELECT Nombre, IdCategoria, Stock, Precio, EsActivo
	FROM (
		VALUES
		('laptop samsung book pro',1,20,2500,1),
		('laptop lenovo idea pad',1,30,2200,1),
		('laptop asus zenbook duo',1,30,2100,1),
		('monitor teros gaming te-2',2,25,1050,1),
		('monitor samsung curvo',2,15,1400,1),
		('monitor huawei gamer',2,10,1350,1),
		('teclado seisen gamer',3,10,800,1),
		('teclado antryx gamer',3,10,1000,1),
		('teclado logitech',3,10,1000,1),
		('auricular logitech gamer',4,15,800,1),
		('auricular hyperx gamer',4,20,680,1),
		('auricular redragon rgb',4,25,950,1),
		('memoria kingston rgb',5,10,200,1),
		('ventilador cooler master',6,20,200,1),
		('mini ventilador lenono',6,15,200,1)
	) AS NuevosProductos (Nombre, IdCategoria, Stock, Precio, EsActivo)
	WHERE NOT EXISTS (
		SELECT 1
		FROM [dbo].[Producto] p
		WHERE p.Nombre = NuevosProductos.Nombre
	);
END
