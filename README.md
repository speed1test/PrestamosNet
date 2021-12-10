Hola, para el desarrollo del presente proyecto se utilizaron los siguientes recursos:
-Server Linux: Ubuntu 20.04
-SQL Server Corriendo en Linux: 15.0.4188.2
    
El Proyecto esta corriendo en la siguiente dirección:
https://registronet.duckdns.org/

Los datos SSH, los enviare por correo:
-Puerto SSH : 22
Comando para acceder a la consola de .NET:
-$screen -R
##################Para acceder a base de datos de forma remota#######################
Host 1: registronet.duckdns.org
ó
Host 2: 207.246.69.79
Puerto para acceder de forma remota a base de datos: 1433
Usuario: sa
Contraseña: Contraseña123*

Uso SQL Server Manager, el cual es versión: 18.10, versión para Windows 10

---->Script para SQL Server Stored Procedures<---

CREATE PROCEDURE dbo.sp_obtener_mes(
	@idMes INT = NULL
)
AS 
SELECT * FROM [DESCRIPCION_POR_VALOR] 
WHERE ID_VALOR=@idMes;

CREATE PROCEDURE dbo.sp_obtener_meses
AS 
SELECT * FROM [DESCRIPCION_POR_VALOR]
GO;

CREATE PROCEDURE dbo.sp_obtener_prestamo(
	@idPrestamo INT = NULL
)
AS 
SELECT * FROM [CUOTA_PRESTAMO] WHERE IDPRESTAMO=@idPrestamo;

CREATE PROCEDURE dbo.sp_obtener_prestamos
AS 
SELECT * FROM [CUOTA_PRESTAMO]
GO;

CREATE PROCEDURE dbo.sp_obtener_tasas
AS 
SELECT * FROM [TASAS_POR_EDAD]
GO;

CREATE PROCEDURE dbo.sp_registrar_consulta(
	@idPrestamo INT = NULL,
	@fechaConsulta DECIMAL(38,2),
	@edadConsulta INT = NULL,
	@ipConsulta NVARCHAR(16) = NULL,
	@valorConsulta DECIMAL(38,2)
)
AS
BEGIN
INSERT INTO [LOG_REGISTRO]
VALUES (@idPrestamo, @fechaConsulta, @edadConsulta, @ipConsulta, @valorConsulta)
END

CREATE PROCEDURE dbo.sp_registrar_log(
	@idPrestamo INT = NULL,
	@fechaConsulta DECIMAL(38,2),
	@edadConsulta INT = NULL,
	@ipConsulta NVARCHAR(16) = NULL,
	@valorConsulta DECIMAL(38,2)
)
AS
BEGIN
/*SET IDENTITY_INSERT LOG_REGISTRO ON*/
/*(IDPRESTAMO,FECHACONSULTA,EDADCONSULTA,IPCONSULTA,VALORCONSULTA)*/
INSERT INTO [LOG_REGISTRO] (IDPRESTAMO,FECHACONSULTA,EDADCONSULTA,IPCONSULTA,VALORCONSULTA)
VALUES (@idPrestamo, @fechaConsulta, @edadConsulta, @ipConsulta, @valorConsulta)
/*SET IDENTITY_INSERT LOG_REGISTRO OFF*/
END

CREATE PROCEDURE dbo.sp_registrar_prestamo(
	@fechaPrestamo NVARCHAR(50) = NULL,
	@montoPrestamo DECIMAL(38,2),
	@mesesPrestamo INT = NULL
)
AS 
BEGIN
INSERT INTO [CUOTA_PRESTAMO]
VALUES (@fechaPrestamo,@montoPrestamo, @mesesPrestamo)
--VALUES ('12-12-2021',123.21, 12)
END

CREATE PROCEDURE dbo.sp_eliminar_prestamo(
	@idPrestamo INT = NULL
)
AS
BEGIN
	DELETE FROM CUOTA_PRESTAMO WHERE IDPRESTAMO = @idPrestamo
END
BEGIN 
	DELETE FROM LOG_REGISTRO WHERE IDPRESTAMO = @idPrestamo
END

CREATE PROCEDURE dbo.sp_obtener_logs
AS 
SELECT * FROM [LOG_REGISTRO]
GO;


