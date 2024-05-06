

create database DBCrudBlazor

use DBCrudBlazor

CREATE TABLE Departamento
(
IdDepartamento int primary key identity(1,1),
Nombre varchar(50) not null
)

CREATE TABLE Empleado
(
IdEmpleado int primary key identity(1,1),
NombreCompleto varchar(50) not null,
IdDepartamento int references Departamento(IdDepartamento) not null,
Sueldo int not null,
FechaContrato date not null
)

insert into Departamento(nombre) values
('Administración'),
('Marketing'),
('Ventas'),
('Comercio')

insert into Empleado(NombreCompleto, IdDepartamento, Sueldo, FechaContrato) VALUES
('Franco Hernandez',1,1400,getDate())

select * from Departamento
select * from Empleado