USE MASTER
GO
CREATE DATABASE dbRegistroEmpresas
GO
USE dbRegistroEmpresas
GO
CREATE TABLE dbo.Empresa(
	Id INT PRIMARY KEY IDENTITY,
	NIT NVARCHAR(14),
    Nombre NVARCHAR(100) NOT NULL,
    RazonSocial NVARCHAR(100) NOT NULL,
    FechaRegistro DATE NOT NULL,
    Bitacora NVARCHAR(MAX)
);
GO

CREATE TABLE dbo.Departamento(
	Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
	Descripcion NVARCHAR(500),
	NivelDeOrganizacion NVARCHAR(100)
);
GO

CREATE TABLE EmpresaDepartamento (
    Id INT PRIMARY KEY IDENTITY,
    IdEmpresa INT,
    IdDepartamento INT,
	NumeroEmpleados INT,
    FOREIGN KEY (IdEmpresa) REFERENCES Empresa(Id),
    FOREIGN KEY (IdDepartamento) REFERENCES Departamento(Id)
);
GO

SELECT * 
FROM dbo.Empresa;

SELECT * 
FROM dbo.Departamento;

SELECT * 
FROM dbo.EmpresaDepartamento;