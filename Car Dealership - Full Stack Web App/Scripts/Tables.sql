USE GuildCars
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='CarMake')
DROP TABLE CarMake
GO

CREATE TABLE CarMake (
MakeID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
MakeName VarChar(50) NOT NULL,
DateAdded DATE NOT NULL,
Id NVARCHAR(128) FOREIGN KEY REFERENCES AspNetUsers(Id) NOT NULL
)

IF EXISTS(SELECT * FROM sys.tables WHERE name='CarModel')
DROP TABLE CarModel
GO

CREATE TABLE CarModel (
ModelID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
MakeID INT FOREIGN KEY REFERENCES CarMake(MakeID) NOT NULL,
ModelName VARCHAR(100) NOT NULL,
Id NVARCHAR(128) FOREIGN KEY REFERENCES AspNetUsers(Id) NOT NULL
)

IF EXISTS(SELECT * FROM sys.tables WHERE name='VehicleType')
DROP TABLE VehicleType
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='BodyType')
DROP TABLE BodyType
GO

CREATE TABLE BodyType (
BodyTypeID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
BodyType VARCHAR(50)
)

CREATE TABLE VehicleType (
TypeID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
TypeName VARCHAR(50) NOT NULL
)

IF EXISTS(SELECT * FROM sys.tables WHERE name='TransmissionType')
DROP TABLE TransmissionType
GO

CREATE TABLE TransmissionType (
TransmissionID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
TransmissionType VARCHAR(10)
)

IF EXISTS(SELECT * FROM sys.tables WHERE name='IntColor')
DROP TABLE IntColor
GO

CREATE TABLE IntColor (
IntColorID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
IntColorName VARCHAR(25) NOT NULL
)

IF EXISTS(SELECT * FROM sys.tables WHERE name='ExtColor')
DROP TABLE ExtColor
GO

CREATE TABLE ExtColor (
ExtColorID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
ExtColorName VARCHAR(25) NOT NULL
)

IF EXISTS(SELECT * FROM sys.tables WHERE name='VehicleInventory')
DROP TABLE VehicleInventory
GO

CREATE TABLE VehicleInventory (
	VehicleID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	MakeID INT FOREIGN KEY REFERENCES CarMake(MakeID) NOT NULL,
	ModelID INT FOREIGN KEY REFERENCES CarModel(ModelID) NOT NULL,
	TypeID INT FOREIGN KEY REFERENCES VehicleType(TypeID) NOT NULL,
	BodyID INT FOREIGN KEY REFERENCES BodyType(BodyTypeID) NOT NULL,
	YearBuilt INT NOT NULL,
	TransmissionID INT FOREIGN KEY REFERENCES TransmissionType(TransmissionID) NOT NULL,
	ExtColorID INT FOREIGN KEY REFERENCES ExtColor(ExtColorID) NOT NULL,
	IntColorID INT FOREIGN KEY REFERENCES IntColor(IntColorID) NOT NULL,
	Mileage INT NOT NULL,
	VINNumber VARCHAR(17) NOT NULL,
	MSRP DECIMAL(10,2) NOT NULL,
	SalesPrice DECIMAL(10,2) NOT NULL,
	VehicleDescription VARCHAR(500) NOT NULL, 
	)

IF EXISTS(SELECT * FROM sys.tables WHERE name='PurchaseType')
DROP TABLE PurchaseType
GO

CREATE TABLE PurchaseType (
PurchaseTypeID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
PurchaseType VARCHAR(50)
)

IF EXISTS(SELECT * FROM sys.tables WHERE name='SalesInformation')
DROP TABLE SalesInformation
GO

CREATE TABLE SalesInformation (
SalesID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
VehicleID INT FOREIGN KEY REFERENCES VehicleInventory(VehicleID) NOT NULL,
CustomerName VARCHAR(100) NOT NULL,
CustomerEmail NVARCHAR(255),
CustomerStreet1 VARCHAR(200) NOT NULL,
CustomerStreet2 VARCHAR(200),
CustomerCity VARCHAR(100) NOT NULL,
CustomerZip CHAR(5) NOT NULL,
PurchasePrice DECIMAL(10,2) NOT NULL,
PurchaseTypeID INT FOREIGN KEY REFERENCES PurchaseType(PurchaseTypeID)
)

IF EXISTS(SELECT * FROM sys.tables WHERE name='ContactUsRequest')
DROP TABLE ContactUsRequest
GO

CREATE TABLE ContactUsRequest (
ContactUsID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
ContactName VARCHAR(100) NOT NULL,
ContactEmail NVARCHAR(255) NOT NULL,
ContactPhone NVARCHAR(22) NOT NULL,
ContactMessage VARCHAR(500) NOT NULL
)

IF EXISTS(SELECT * FROM sys.tables WHERE name='SalesSpecials')
DROP TABLE SalesSpecials
GO

CREATE TABLE SalesSpecials (
SpecialID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
SpecialName VARCHAR(200) NOT NULL,
SpecialDescription VARCHAR(500) NOT NULL
)