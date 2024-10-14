IF EXISTS (SELECT 1 FROM sys.tables WHERE name like 'Usuario')
BEGIN
	INSERT INTO [dbo].[Usuario] (NombreCompleto, Correo, IdRol, Clave)
	SELECT NombreCompleto, Correo, IdRol, Clave
	FROM (
		VALUES
		('codigo estudiante','code@example.com',1,'123')
	) AS NuevosRoles (NombreCompleto, Correo, IdRol, Clave)
	WHERE NOT EXISTS (
		SELECT 1
		FROM [dbo].[Usuario] u
		WHERE u.Correo = NuevosRoles.Correo
	);
END
