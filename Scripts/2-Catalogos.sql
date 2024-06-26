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
('Atención al Cliente', 'Encargado de interactuar directamente con los clientes para resolver consultas, ', 'Operativo'),
('Producción', 'Responsable de la fabricación o creación de productos de la empresa según las especificaciones y estándares establecidos, optimizando los procesos para cumplir con la demanda y los plazos de entrega, ', 'Operativo'),
('Recursos Humanos', 'Gestiona las políticas y prácticas de recursos humanos de la empresa, incluyendo la contratación, formación, desarrollo del personal, compensaciones y beneficios, así como las relaciones laborales., ', 'Funcional'),
('Marketing y Comunicación', 'Encargado de desarrollar estrategias de marketing, publicidad y comunicación para promover los productos o servicios de la empresa, así como gestionar la imagen de marca y las relaciones públicas, ', 'Divisional'),
('Oficina de Gestión de Proyectos (Project Management Office - PMO)', 'Coordina y estandariza la gestión de proyectos en toda la organización, asegurando la alineación con los objetivos estratégicos de la empresa, el uso eficiente de recursos y la mejora continua de procesos, ', 'Corporativo'),
('Desarrollo Corporativo o Estratégico', 'Se enfoca en el desarrollo de nuevas oportunidades de negocio, alianzas estratégicas, fusiones y adquisiciones, identificación de mercados emergentes y gestión de la innovación dentro de la empresa, ', 'Estratégico')

/*
--Prueba EmpresaDepartamento
INSERT INTO EmpresaDepartamento(IdEmpresa, IdDepartamento, NumeroEmpleados)
VALUES
(1,1,20),
(1,2,15),
(1,3,10)
*/
