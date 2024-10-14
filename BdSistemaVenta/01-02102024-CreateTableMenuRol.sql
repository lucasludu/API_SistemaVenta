IF EXISTS (SELECT 1 FROM sys.tables WHERE name like 'MenuRol')
BEGIN
	DROP TABLE [dbo].[MenuRol]
END 
GO

CREATE TABLE [dbo].[MenuRol] (
	IdMenuRol INT IDENTITY(1,1),
	IdMenu INT,
	IdRol INT,
	CONSTRAINT pk_MenuRol PRIMARY KEY (IdMenuRol),
	CONSTRAINT fk_MenuRol_Menu FOREIGN KEY (IdMenu) REFERENCES [dbo].[Menu] (IdMenu),
	CONSTRAINT fk_MenuRol_Rol FOREIGN KEY (IdRol) REFERENCES [dbo].[Rol] (IdRol)
);
