IF EXISTS (SELECT 1 FROM sys.tables WHERE name like 'NumeroDocumento')
BEGIN

	INSERT INTO [dbo].[NumeroDocumento] (UltimoRegistro, FechaRegistro) VALUES
	(0,getdate())

END
