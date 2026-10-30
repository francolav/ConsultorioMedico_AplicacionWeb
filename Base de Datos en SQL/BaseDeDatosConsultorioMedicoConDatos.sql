Create database ConsultorioMedico_appWeb
go 
create table Especialidad (
	IdEspecialidad int primary key identity (1,1),
	Nombre Varchar (50) unique,
	Eliminado bit
	)
go
create table Medico (
	IdMedico int identity (1,1) Primary Key,
	Apellido Varchar(50) not null,
	Nombre Varchar(50) not null,
	Matricula Varchar(50),
	Dni Varchar (20) Unique not null,
	Telefono Varchar (20) not null,
	TelefonoAlternativo Varchar(20),
	Email Varchar(20),
	Eliminado bit
	)
create table Especialidades (
	IdMedico int  references Medico(IdMedico),
	IdEspecialidad int  references Especialidad(IdEspecialidad),
	primary key (idMedico,IdEspecialidad)
	)
go
create table ObraSocial (
	IdObraSocial int identity (1,1) primary key,
	Nombre varchar(50) not null,
	Eliminado bit
	)
go
Create Table Paciente (
	IdPaciente int identity (1,1) Primary Key,
	IdObraSocial int Foreign key references ObraSocial(IdObraSocial) not null,
	Apellido Varchar(50),
	Nombre Varchar(50),
	Telefono Varchar(50),
	TelefonoAlternativo Varchar(50),
	Email Varchar(50),
	FechaNacimiento Date,
	Eliminado bit
	)
go
Create Table Historial(
	IdHistorial int identity (1,1) Primary key,
	IdPaciente int Foreign key references Paciente(IdPaciente),
	IdMedico int Foreign key references Medico(IdMedico),
	Fecha date not null,
	Diagnostico Varchar(200)
	)
go
Create Table AgendaMedico (
	IdAgendaMedico int identity (1,1) Primary key,
	IdMedico int Foreign key references Medico(IdMedico) not null,
	IdEspecialidad int Foreign key references Especialidad(IdEspecialidad) not null,
	DiaAtencion Int not null,
	HoraInicio Time not null,
	HoraFin Time not null,
	Eliminado bit
	)
go
Create Table Turno (
	IdTurno int Identity (1,1) Primary key,
	IdPaciente int Foreign Key references Paciente(IdPaciente) not null,
	IdMedico int Foreign Key references Medico(IdMedico) not null,
	Fecha Date not null,
	Hora Time not null,
	Estado Int not null
	)


-- 1. Insertar Especialidades
INSERT INTO Especialidad (Nombre, Eliminado) VALUES ('Pediatría', 0);
INSERT INTO Especialidad (Nombre, Eliminado) VALUES ('Cardiología', 0);

-- 2. Insertar Médicos (Dr. Pérez y Dra. Gómez)
INSERT INTO Medico (Apellido, Nombre, Matricula, Dni, Telefono, Eliminado) 
VALUES ('Pérez', 'Juan', 'M1234', '30111222', '11223344', 0);

INSERT INTO Medico (Apellido, Nombre, Matricula, Dni, Telefono, Eliminado) 
VALUES ('Gómez', 'María', 'M5678', '35444555', '55667788', 0);

-- 3. Relacionar varias Especialidades (El Dr. Pérez tiene AMBAS especialidades)
INSERT INTO Especialidades (IdMedico, IdEspecialidad) VALUES (1, 1); -- Pérez es Pediatra
INSERT INTO Especialidades (IdMedico, IdEspecialidad) VALUES (1, 2); -- Pérez también es Cardiólogo
INSERT INTO Especialidades (IdMedico, IdEspecialidad) VALUES (2, 1); -- Gómez es Pediatra

-- 4. Insertar Obras Sociales
INSERT INTO ObraSocial (Nombre, Eliminado) VALUES ('OSDE', 0);

-- 5. Insertar un Paciente
INSERT INTO Paciente (IdObraSocial, Apellido, Nombre, Telefono, Eliminado) 
VALUES (1, 'González', 'Lucas', '99887766', 0);

-- 6. Asignar Agenda (El Dr. Pérez atiende Pediatría los Lunes y Cardiología los Miércoles)
INSERT INTO AgendaMedico (IdMedico, IdEspecialidad, DiaAtencion, HoraInicio, HoraFin, Eliminado)
VALUES (1, 1, 1, '08:00:00', '12:00:00', 0); -- Lunes Pediatría

INSERT INTO AgendaMedico (IdMedico, IdEspecialidad, DiaAtencion, HoraInicio, HoraFin, Eliminado)
VALUES (1, 2, 3, '14:00:00', '18:00:00', 0); -- Miércoles Cardiología

-- 7. Crear un Turno para Lucas con el Dr. Pérez
INSERT INTO Turno (IdPaciente, IdMedico, Fecha, Hora, Estado)
VALUES (1, 1, '2026-11-02', '09:00:00', 1); -- Estado 1 = Reservado

-- 8. Crear un Historial Clínico (Cuando Lucas asiste a la consulta)
INSERT INTO Historial (IdPaciente, IdMedico, Fecha, Diagnostico)
VALUES (1, 1, '2026-11-02', 'Control de rutina infantil. Todo normal.');

SELECT m.Nombre AS Medico_Nombre, m.Apellido AS Medico_Apellido, e.Nombre AS Especialidad
FROM Medico m
INNER JOIN Especialidades ee ON m.IdMedico = ee.IdMedico
INNER JOIN Especialidad e ON ee.IdEspecialidad = e.IdEspecialidad;
Use ConsultorioMedico_appWeb
go
INSERT INTO Especialidad (Nombre, Eliminado) VALUES ('Clínica Médica', 0); -- Id: 3
INSERT INTO Especialidad (Nombre, Eliminado) VALUES ('Dermatología', 0);   -- Id: 4
INSERT INTO Especialidad (Nombre, Eliminado) VALUES ('Traumatología', 0);  -- Id: 5

INSERT INTO Medico (Apellido, Nombre, Matricula, Dni, Telefono, Eliminado) 
VALUES ('Rodríguez', 'Carlos', 'M9012', '28111222', '11334455', 0); -- Id: 3

INSERT INTO Medico (Apellido, Nombre, Matricula, Dni, Telefono, Eliminado) 
VALUES ('Fernández', 'Laura', 'M3456', '32444555', '11556677', 0); -- Id: 4

INSERT INTO Medico (Apellido, Nombre, Matricula, Dni, Telefono, Eliminado) 
VALUES ('López', 'Andrés', 'M7890', '25666777', '11778899', 0);    -- Id: 5

-- El Dr. Rodríguez es Clínico y Traumatólogo
INSERT INTO Especialidades (IdMedico, IdEspecialidad) VALUES (3, 3); 
INSERT INTO Especialidades (IdMedico, IdEspecialidad) VALUES (3, 5); 

-- La Dra. Fernández es Dermatóloga
INSERT INTO Especialidades (IdMedico, IdEspecialidad) VALUES (4, 4); 

-- El Dr. López es Clínico
INSERT INTO Especialidades (IdMedico, IdEspecialidad) VALUES (5, 3); 


-- LUNES (Ya cubierto por el Dr. Pérez en Pediatría. Sumamos al Dr. Rodríguez en Clínica)
INSERT INTO AgendaMedico (IdMedico, IdEspecialidad, DiaAtencion, HoraInicio, HoraFin, Eliminado)
VALUES (3, 3, 1, '09:00:00', '13:00:00', 0);

-- MARTES (Dra. Fernández - Dermatología)
INSERT INTO AgendaMedico (IdMedico, IdEspecialidad, DiaAtencion, HoraInicio, HoraFin, Eliminado)
VALUES (4, 4, 2, '08:00:00', '14:00:00', 0);

-- MIÉRCOLES (Ya cubierto por el Dr. Pérez en Cardiología. Sumamos al Dr. López en Clínica)
INSERT INTO AgendaMedico (IdMedico, IdEspecialidad, DiaAtencion, HoraInicio, HoraFin, Eliminado)
VALUES (5, 3, 3, '14:00:00', '20:00:00', 0);

-- JUEVES (Dr. Rodríguez - Traumatología)
INSERT INTO AgendaMedico (IdMedico, IdEspecialidad, DiaAtencion, HoraInicio, HoraFin, Eliminado)
VALUES (3, 5, 4, '10:00:00', '16:00:00', 0);

-- VIERNES (Dra. Fernández - Dermatología)
INSERT INTO AgendaMedico (IdMedico, IdEspecialidad, DiaAtencion, HoraInicio, HoraFin, Eliminado)
VALUES (4, 4, 5, '12:00:00', '18:00:00', 0);

-- SÁBADO (Dr. López - Clínica Médica por la mañana)
INSERT INTO AgendaMedico (IdMedico, IdEspecialidad, DiaAtencion, HoraInicio, HoraFin, Eliminado)
VALUES (5, 3, 6, '08:00:00', '13:00:00', 0);

-- DOMINGO (Guardia de Clínica Médica con el Dr. Rodríguez)
INSERT INTO AgendaMedico (IdMedico, IdEspecialidad, DiaAtencion, HoraInicio, HoraFin, Eliminado)
VALUES (3, 3, 7, '08:00:00', '20:00:00', 0);



sql
INSERT INTO Especialidad (Nombre, Eliminado) VALUES ('Clínica Médica', 0); -- Id: 3
INSERT INTO Especialidad (Nombre, Eliminado) VALUES ('Dermatología', 0);   -- Id: 4
INSERT INTO Especialidad (Nombre, Eliminado) VALUES ('Traumatología', 0);  -- Id: 5


sql
INSERT INTO Medico (Apellido, Nombre, Matricula, Dni, Telefono, Eliminado) 
VALUES ('Rodríguez', 'Carlos', 'M9012', '28111222', '11334455', 0); -- Id: 3

INSERT INTO Medico (Apellido, Nombre, Matricula, Dni, Telefono, Eliminado) 
VALUES ('Fernández', 'Laura', 'M3456', '32444555', '11556677', 0); -- Id: 4

INSERT INTO Medico (Apellido, Nombre, Matricula, Dni, Telefono, Eliminado) 
VALUES ('López', 'Andrés', 'M7890', '25666777', '11778899', 0);    -- Id: 5
Usa el código con precaución.

3. Asignación de Especialidades a los Nuevos Médicos

sql
-- El Dr. Rodríguez es Clínico y Traumatólogo
INSERT INTO Especialidades (IdMedico, IdEspecialidad) VALUES (3, 3); 
INSERT INTO Especialidades (IdMedico, IdEspecialidad) VALUES (3, 5); 

-- La Dra. Fernández es Dermatóloga
INSERT INTO Especialidades (IdMedico, IdEspecialidad) VALUES (4, 4); 

-- El Dr. López es Clínico
INSERT INTO Especialidades (IdMedico, IdEspecialidad) VALUES (5, 3); 
Usa el código con precaución.

4. Agendas Médicas (Distribuidas para cubrir toda la semana)

Nota: Asumiremos la convención estándar donde 1 = Lunes, 2 = Martes, ..., 6 = Sábado, 7 = Domingo.
sql
-- LUNES (Ya cubierto por el Dr. Pérez en Pediatría. Sumamos al Dr. Rodríguez en Clínica)
INSERT INTO AgendaMedico (IdMedico, IdEspecialidad, DiaAtencion, HoraInicio, HoraFin, Eliminado)
VALUES (3, 3, 1, '09:00:00', '13:00:00', 0);

-- MARTES (Dra. Fernández - Dermatología)
INSERT INTO AgendaMedico (IdMedico, IdEspecialidad, DiaAtencion, HoraInicio, HoraFin, Eliminado)
VALUES (4, 4, 2, '08:00:00', '14:00:00', 0);

-- MIÉRCOLES (Ya cubierto por el Dr. Pérez en Cardiología. Sumamos al Dr. López en Clínica)
INSERT INTO AgendaMedico (IdMedico, IdEspecialidad, DiaAtencion, HoraInicio, HoraFin, Eliminado)
VALUES (5, 3, 3, '14:00:00', '20:00:00', 0);

-- JUEVES (Dr. Rodríguez - Traumatología)
INSERT INTO AgendaMedico (IdMedico, IdEspecialidad, DiaAtencion, HoraInicio, HoraFin, Eliminado)
VALUES (3, 5, 4, '10:00:00', '16:00:00', 0);

-- VIERNES (Dra. Fernández - Dermatología)
INSERT INTO AgendaMedico (IdMedico, IdEspecialidad, DiaAtencion, HoraInicio, HoraFin, Eliminado)
VALUES (4, 4, 5, '12:00:00', '18:00:00', 0);

-- SÁBADO (Dr. López - Clínica Médica por la mañana)
INSERT INTO AgendaMedico (IdMedico, IdEspecialidad, DiaAtencion, HoraInicio, HoraFin, Eliminado)
VALUES (5, 3, 6, '08:00:00', '13:00:00', 0);

-- DOMINGO (Guardia de Clínica Médica con el Dr. Rodríguez)
INSERT INTO AgendaMedico (IdMedico, IdEspecialidad, DiaAtencion, HoraInicio, HoraFin, Eliminado)
VALUES (3, 3, 7, '08:00:00', '20:00:00', 0);

SELECT 
    CASE a.DiaAtencion
        WHEN 1 THEN 'Lunes'
        WHEN 2 THEN 'Martes'
        WHEN 3 THEN 'Miércoles'
        WHEN 4 THEN 'Jueves'
        WHEN 5 THEN 'Viernes'
        WHEN 6 THEN 'Sábado'
        WHEN 7 THEN 'Domingo'
    END AS Dia,
    m.Nombre + ' ' + m.Apellido AS Medico,
    e.Nombre AS Especialidad,
    a.HoraInicio,
    a.HoraFin
FROM AgendaMedico a
INNER JOIN Medico m ON a.IdMedico = m.IdMedico
INNER JOIN Especialidad e ON a.IdEspecialidad = e.IdEspecialidad
WHERE a.Eliminado = 0
ORDER BY a.DiaAtencion, a.HoraInicio;
