IF EXISTS (SELECT 1 FROM sys.tables WHERE name like 'Menu')
BEGIN
	DROP TABLE [dbo].[Menu]
END 
GO

CREATE TABLE [dbo].[Menu] (
	IdMenu INT IDENTITY(1,1),
	Nombre VARCHAR(50),
	Icono VARCHAR(50),
	Url VARCHAR(50),
	CONSTRAINT pk_Menu PRIMARY KEY (IdMenu)
);
