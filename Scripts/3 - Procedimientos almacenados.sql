USE dbRegistroEmpresas
GO
/*EMPRESA*/
--OBTENER EMPRESA
CREATE PROCEDURE ObtenerEmpresaPorNIT
	@NIT NVARCHAR(14)
AS
BEGIN
    SELECT Nombre, RazonSocial, FechaRegistro, Bitacora
	FROM dbo.Empresa WHERE NIT=@NIT
END;
GO
/*
	EXEC ObtenerEmpresaPorNIT @NIT = '00000000000001';
	GO
*/

/*EMPRESA*/
--OBTENER DEPARTAMENTOS POR NIT EMPRESA
CREATE PROCEDURE ObtenerDepartamentosPorNIT
	@NIT NVARCHAR(14)
AS
BEGIN
    SELECT departamento.Nombre,
		   departamento.Descripcion,
		   departamento.NivelDeOrganizacion,
		   ed.NumeroEmpleados
	FROM dbo.EmpresaDepartamento ed
    INNER JOIN Empresa empresa ON ed.IdEmpresa = empresa.Id
	INNER JOIN Departamento departamento ON ed.IdDepartamento = departamento.Id
	AND empresa.NIT=@NIT
END;
GO
/*
	EXEC ObtenerDepartamentosPorNIT @NIT = '00000000000001';
	GO
*/
