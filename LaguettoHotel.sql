CREATE DATABASE LaguettoHotelWeb
GO

USE LaguettoHotelWeb
GO


--SELECT * FROM Cliente
--SELECT * FROM Reserva
--SELECT * FROM loginUser


CREATE TABLE Cliente (
    idCliente INTEGER IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Nome Varchar (50) not null,
    CPF Varchar (11) not null,
    Telefone Varchar (15) not null,
    CEP Varchar (8) not null, 
	Logradouro Varchar (50) not null,
	Bairro Varchar (50) not null,
	Cidade Varchar (50) not null,
	UF Varchar (2) not null,
);

GO
CREATE TABLE Reserva (
	idQuarto Int IDENTITY(1,1) PRIMARY KEY,
	Nome Varchar (50) not null,
    CPF Varchar (11) not null,		
	Quarto Varchar (4) not null,
	valorDiaria Varchar (10) not null,
	valorTotal Varchar (10) not null,
	dataEntrada Date not null,
	dataSaida Date not null,
);

GO
CREATE TABLE loginUser
(
	idUser int IDENTITY(1,1) PRIMARY KEY,
	Username VARCHAR (50) not null,
	password varchar(100) not null,
	email varchar(100) not null,
	tipoUsuario char(1) null,
	admin char(1) not null,
	foreignId int null
);

GO

