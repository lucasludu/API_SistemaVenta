IF EXISTS (SELECT 1 FROM sys.tables WHERE name like 'Categoria')
BEGIN
	INSERT INTO [dbo].[Categoria] (Nombre, EsActivo)
	SELECT Nombre, EsActivo
	FROM (
		VALUES
		('Laptops',1),
		('Monitores',1),
		('Teclados',1),
		('Auriculares',1),
		('Memorias',1),
		('Accesorios',1)
	) AS NuevasCategorias (Nombre, EsActivo)
	WHERE NOT EXISTS (
		SELECT 1
		FROM [dbo].[Categoria] c
		WHERE c.Nombre = NuevasCategorias.Nombre
	);
END
