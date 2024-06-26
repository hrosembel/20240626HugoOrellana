USE dbRegistroEmpresas
GO

/*
--Prueba Empresa
INSERT INTO Empresa(NIT,Nombre, RazonSocial, FechaRegistro, Bitacora)
VALUES('00000000000001','TEST COMPANY 1','Fundacion', GETDATE(),'')
*/

-- Departamentos
INSERT INTO Departamento(Nombre,Descripcion, NivelDeOrganizacion)
VALUES 
('Atenci�n al Cliente', 'Encargado de interactuar directamente con los clientes para resolver consultas, ', 'Operativo'),
('Producci�n', 'Responsable de la fabricaci�n o creaci�n de productos de la empresa seg�n las especificaciones y est�ndares establecidos, optimizando los procesos para cumplir con la demanda y los plazos de entrega, ', 'Operativo'),
('Recursos Humanos', 'Gestiona las pol�ticas y pr�cticas de recursos humanos de la empresa, incluyendo la contrataci�n, formaci�n, desarrollo del personal, compensaciones y beneficios, as� como las relaciones laborales., ', 'Funcional'),
('Marketing y Comunicaci�n', 'Encargado de desarrollar estrategias de marketing, publicidad y comunicaci�n para promover los productos o servicios de la empresa, as� como gestionar la imagen de marca y las relaciones p�blicas, ', 'Divisional'),
('Oficina de Gesti�n de Proyectos (Project Management Office - PMO)', 'Coordina y estandariza la gesti�n de proyectos en toda la organizaci�n, asegurando la alineaci�n con los objetivos estrat�gicos de la empresa, el uso eficiente de recursos y la mejora continua de procesos, ', 'Corporativo'),
('Desarrollo Corporativo o Estrat�gico', 'Se enfoca en el desarrollo de nuevas oportunidades de negocio, alianzas estrat�gicas, fusiones y adquisiciones, identificaci�n de mercados emergentes y gesti�n de la innovaci�n dentro de la empresa, ', 'Estrat�gico')

/*
--Prueba EmpresaDepartamento
INSERT INTO EmpresaDepartamento(IdEmpresa, IdDepartamento, NumeroEmpleados)
VALUES
(1,1,20),
(1,2,15),
(1,3,10)
*/
