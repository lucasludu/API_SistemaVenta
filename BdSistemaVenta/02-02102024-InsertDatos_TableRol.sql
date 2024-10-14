IF EXISTS (SELECT 1 FROM sys.tables WHERE name like 'Rol')
BEGIN
	INSERT INTO [dbo].[ROL] (Nombre)
	SELECT Nombre
	FROM (
		VALUES
		('Administrador'),
		('Empleado'),
		('Supervisor')
	) AS NuevosRoles (Nombre)
	WHERE NOT EXISTS (
		SELECT 1
		FROM [dbo].[ROL] r
		WHERE r.Nombre = NuevosRoles.Nombre
	);
END
