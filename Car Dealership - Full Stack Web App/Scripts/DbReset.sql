USE GuildCars
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DbReset')
   DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset AS
   BEGIN
	DELETE FROM SalesInformation;
	DELETE FROM VehicleInventory;
	DELETE FROM CarModels;
	DELETE FROM CarMakes;
	DELETE FROM BodyTypes;
	DELETE FROM SalesTypes;
	DELETE FROM TransmissionTypes;
	DELETE FROM ExtColors;
	DELETE FROM IntColors;
	DELETE FROM SalesSpecials;
	DELETE FROM ContactUsRequest;
	DELETE FROM PurchaseTypes;
	
	DBCC CHECKIDENT ('CarMakes', RESEED, 1)
	DBCC CHECKIDENT ('VehicleInventory', RESEED, 1)
	DBCC CHECKIDENT ('CarModels', RESEED, 1)
	DBCC CHECKIDENT ('SalesSpecials', RESEED, 1)
	DBCC CHECKIDENT ('ContactUsRequest', RESEED, 1)
	DBCC CHECKIDENT ('SalesInformation', RESEED, 1)

	SET IDENTITY_INSERT CarMakes ON;

	INSERT INTO CarMakes (MakeID, MakeName, DateAdded, Email)
	VALUES (1, 'Acura', '4/15/2020', 'test@test.com'),
	(2, 'Audi', '4/15/2020','test@test.com'),
	(3, 'BMW', '4/15/2020', 'test2@test.com')

	SET IDENTITY_INSERT CarMakes OFF;

	SET IDENTITY_INSERT CarModels ON;

	INSERT INTO CarModels(ModelID, MakeID, ModelName, DateAdded, Email)
	VALUES (1, 1, 'MDX', '4/15/2020', 'test@test.com'),
	(2, 1, 'RDX', '4/15/2020', 'test@test.com'),
	(3, 1, 'TLX', '4/15/2020', 'test2@test.com'),
	(4, 2, 'A3', '4/15/2020', 'test@test.com')

	SET IDENTITY_INSERT CarModels OFF;

	SET IDENTITY_INSERT SalesTypes ON;

	INSERT INTO SalesTypes(SalesTypeID, SalesTypeName)
	VALUES(1, 'New'),
	(2, 'Used')

	SET IDENTITY_INSERT SalesTypes OFF;

	SET IDENTITY_INSERT BodyTypes ON;

	INSERT INTO BodyTypes (BodyTypeID, BodyTypeName)
	VALUES (1,'Car'),
	(2,'SUV'),
	(3,'Truck'),
	(4, 'Van')

	SET IDENTITY_INSERT BodyTypes OFF;

	SET IDENTITY_INSERT TransmissionTypes ON;

	INSERT INTO	TransmissionTypes(TransmissionID, TransmissionTypeName)
	Values(1, 'Automatic'),
	(2, 'Manual')

	SET IDENTITY_INSERT TransmissionTypes OFF;

	SET IDENTITY_INSERT ExtColors ON;

	INSERT INTO ExtColors(ExtColorID, ExtColorName)
	VALUES(1, 'White'),
	(2, 'Black'),
	(3, 'Gray'),
	(4, 'Silver'),
	(5, 'Red'),
	(6, 'Blue')

	SET IDENTITY_INSERT ExtColors OFF;

	SET IDENTITY_INSERT IntColors ON;

	INSERT INTO IntColors(IntColorID, IntColorName)
	VALUES(1, 'Black'),
	(2, 'White'),
	(3, 'Gray'),
	(4, 'Tan'),
	(5, 'Brown')

	SET IDENTITY_INSERT IntColors OFF;

	SET IDENTITY_INSERT VehicleInventory ON;

	INSERT INTO VehicleInventory(VehicleID,MakeID,ModelID,SalesTypeID,BodyTypeID,YearBuilt,TransmissionID,ExtColorID,IntColorID,Mileage,VINNumber,MSRP,SalesPrice,VehicleDescription,IsFeaturedVehicle,ImageFileName)
	VALUES(1, 1, 1, 1, 2, 2020, 1, 1, 3, 46882, '5J8TB4H3XHL018174',25620,22350, 'Nice and Clean Acura MDX',0,'placeholder.png'),
	
	(2, 1, 2, 2, 1, 2020, 1, 5, 2, 51243, '5J8TB4H3XHL018174',19500, 19000, 'Brand New RDX',1,'placeholder.png'),
	
	(3, 1, 3, 2, 2, 2018, 1, 4, 1, 22638, '5J8TB4H3XHL018174',17500,16000, 'Very Sweet TLX',1,'placeholder.png'),
	
	(4, 2, 4, 2, 2, 2015, 1, 2, 1, 19218, '5J8TB4H3XHL018174',21900,20000, 'Nice and Clean Acura RDX',1,'placeholder.png');


	SET IDENTITY_INSERT VehicleInventory OFF;

	SET IDENTITY_INSERT SalesSpecials ON;

	INSERT INTO SalesSpecials(SpecialID, SpecialName, SpecialDescription)
	VALUES(1, 'Acura MDX Special', '2.9% AND $750 Customer Rebate on a New MDX!'),
	(2, 'Cash Back Special', 'Pay with Cash and Receive a $1,000 Rebate')

	SET IDENTITY_INSERT SalesSpecials OFF;

	SET IDENTITY_INSERT ContactUsRequest ON;

	INSERT INTO ContactUsRequest(ContactUsID, ContactName, ContactEmail, ContactPhone, ContactMessage)
	VALUES(1, 'Bob Smith', 'bob@bobsmith.com', '502-111-1111', 'I am interested in the used Acura MDX')

	SET IDENTITY_INSERT ContactUsRequest OFF;

	SET IDENTITY_INSERT PurchaseTypes ON;

	INSERT INTO PurchaseTypes(PurchaseTypeID, PurchaseTypeName)
	VALUES(1, 'Bank Finance'),
	(2, 'Cash'),
	(3, 'Dealer Finance')

	SET IDENTITY_INSERT PurchaseTypes OFF;

	SET IDENTITY_INSERT SalesInformation ON;

	INSERT INTO SalesInformation(SalesID, CustomerName, CustomerPhone, CustomerEmail, CustomerStreet1, CustomerStreet2, CustomerCity, CustomerState,
	CustomerZip, PurchasePrice, PurchaseTypeID, VehicleID, PurchaseDate, Id)
	Values(1, 'Monica Timms', '502-555-0156', 'mtibbs@gmail.com', '123 E Main St', null, 'Louisville', 'KY', '40204', 15500, 1, 3, '5/4/2020', '0c79a30f-8399-4b6f-9363-1f7c604a5d53'),
	(2, 'Carolyn Sharp', '502-555-0148', 'carolyn.sharp@gmail.com', '517 E Jackson St', null, 'Louisville', 'KY', '40202', 18250, 3, 2, '4/25/2020', '0c79a30f-8399-4b6f-9363-1f7c604a5d53')

	SET IDENTITY_INSERT SalesInformation OFF;

END

