USE master
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name='DVDLibrary')
DROP DATABASE DVDLibrary
GO

CREATE DATABASE DVDLibrary
GO

USE DVDLibrary
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Dvds')
DROP TABLE Dvds
GO

CREATE TABLE Dvds (
DvdID INT IDENTITY(0,1) PRIMARY KEY NOT NULL,
Title varchar(50) NOT NULL,
ReleaseYear INT NOT NULL,
Director varchar(50) NOT NULL,
Rating varchar(5) NOT NULL,
Notes varchar(500) NOT NULL
)
GO

