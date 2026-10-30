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
