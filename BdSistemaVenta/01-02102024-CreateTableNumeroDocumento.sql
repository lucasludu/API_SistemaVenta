IF EXISTS (SELECT 1 FROM sys.tables WHERE name like 'NumeroDocumento')
BEGIN
	DROP TABLE [dbo].[NumeroDocumento]
END 
GO

CREATE TABLE [dbo].[NumeroDocumento] (
	IdNumeroDocumento INT IDENTITY(1,1),
	UltimoRegistro INT NOT NULL,
	FechaRegistro DATETIME DEFAULT GETDATE(),
	CONSTRAINT pk_NumeroDocumento PRIMARY KEY (IdNumeroDocumento)
);
