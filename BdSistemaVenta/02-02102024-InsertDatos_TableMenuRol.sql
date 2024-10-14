IF EXISTS (SELECT 1 FROM sys.tables WHERE name like 'MenuRol')
BEGIN
	INSERT INTO [dbo].[MenuRol] (IdMenu, IdRol)
	SELECT IdMenu, IdRol
	FROM (
		VALUES
		(1,1),
		(2,1),
		(3,1),
		(4,1),
		(5,1),
		(6,1),
		(4,2),
		(5,2),
		(3,3),
		(4,3),
		(5,3),
		(6,3)
	) AS NuevosMenuRol (IdMenu, IdRol)
	WHERE NOT EXISTS (
		SELECT 1
		FROM [dbo].[MenuRol] mr
		WHERE mr.IdMenu = NuevosMenuRol.IdMenu AND mr.IdRol = NuevosMenuRol.IdRol
	);
END
