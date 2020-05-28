USE GuildCars
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'BodyTypesSelectAll')

BEGIN
   DROP PROCEDURE BodyTypesSelectAll
END
GO
 
CREATE PROCEDURE BodyTypesSelectAll
AS

SELECT * FROM BodyTypes
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarMakesSelectAll')

BEGIN
   DROP PROCEDURE CarMakesSelectAll
END
GO
 
CREATE PROCEDURE CarMakesSelectAll
AS

Select * from CarMakes

GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarModelItemSelectAll')

BEGIN
   DROP PROCEDURE CarModelItemSelectAll
END
GO
 
CREATE PROCEDURE CarModelItemSelectAll
AS

Select CarModels.ModelID, CarMakes.MakeName, CarModels.ModelName, CarModels.DateAdded, CarModels.Email
FROM CarModels
	INNER JOIN CarMakes ON
CarModels.MakeID = CarMakes.MakeID
	
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SalesTypesSelectAll')

BEGIN
   DROP PROCEDURE SalesTypesSelectAll
END
GO
 
CREATE PROCEDURE SalesTypesSelectAll
AS

SELECT * FROM SalesTypes
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'TransmissionTypesSelectAll')

BEGIN
   DROP PROCEDURE TransmissionTypesSelectAll
END
GO
 
CREATE PROCEDURE TransmissionTypesSelectAll
AS

SELECT * FROM TransmissionTypes
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ExtColorsSelectAll')

BEGIN
   DROP PROCEDURE ExtColorsSelectAll
END
GO
 
CREATE PROCEDURE ExtColorsSelectAll
AS

SELECT * FROM ExtColors
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'IntColorsSelectAll')
BEGIN
   DROP PROCEDURE IntColorsSelectAll
END
GO
 
CREATE PROCEDURE IntColorsSelectAll
AS

SELECT * FROM IntColors
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleInventorySelectAll')
BEGIN
   DROP PROCEDURE VehicleInventorySelectAll
END
GO
 
CREATE PROCEDURE VehicleInventorySelectAll
AS

SELECT *
FROM VehicleInventory
WHERE VehicleID NOT IN
	(SELECT VehicleID
	FROM SalesInformation)

GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleInsert')
BEGIN
   DROP PROCEDURE VehicleInsert
END
GO
 
CREATE PROCEDURE VehicleInsert (
	@VehicleID int output,
	@MakeID int,
	@ModelID int,
	@SalesTypeID int,
	@BodyTypeID int,
	@YearBuilt int,
	@TransmissionID int,
	@ExtColorID int,
	@IntColorID int,
	@Mileage int,
	@VINNumber varchar(17),
	@MSRP decimal(10,2),
	@SalesPrice decimal(10,2),
	@VehicleDescription varchar(500),
	@IsFeaturedVehicle bit,
	@ImageFileName varchar(100))
AS
BEGIN

INSERT INTO VehicleInventory(MakeID,ModelID,SalesTypeID,BodyTypeID,YearBuilt,TransmissionID,ExtColorID,IntColorID,Mileage,VINNumber,MSRP,SalesPrice,VehicleDescription,IsFeaturedVehicle,ImageFileName)
VALUES(@MakeID, @ModelID, @SalesTypeID, @BodyTypeID, @YearBuilt, @TransmissionID, @ExtColorID, @IntColorID, @Mileage, @VINNumber, @MSRP, @SalesPrice, @VehicleDescription, @IsFeaturedVehicle, @ImageFileName)

SET @VehicleID = SCOPE_IDENTITY();

END
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleUpdate')
BEGIN
   DROP PROCEDURE VehicleUpdate
END
GO
 
CREATE PROCEDURE VehicleUpdate (
	@VehicleID int,
	@MakeID int,
	@ModelID int,
	@SalesTypeID int,
	@BodyTypeID int,
	@YearBuilt int,
	@TransmissionID int,
	@ExtColorID int,
	@IntColorID int,
	@Mileage int,
	@VINNumber varchar(17),
	@MSRP decimal(10,2),
	@SalesPrice decimal(10,2),
	@VehicleDescription varchar(500),
	@IsFeaturedVehicle bit,
	@ImageFileName varchar(100))
AS
BEGIN

UPDATE VehicleInventory SET
	MakeID = @MakeID,
	ModelID = @ModelID,
	SalesTypeID = @SalesTypeID,
	BodyTypeID = @BodyTypeID,
	YearBuilt = @YearBuilt,
	TransmissionID = @TransmissionID,
	ExtColorID = @ExtColorID,
	IntColorID = @IntColorID,
	Mileage = @Mileage,
	VINNumber = @VINNumber,
	MSRP = @MSRP,
	SalesPrice = @SalesPrice,
	VehicleDescription = @VehicleDescription,
	IsFeaturedVehicle = @IsFeaturedVehicle,
	ImageFileName = @ImageFileName
WHERE VehicleID = @VehicleID

END 

GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleDelete')
BEGIN
   DROP PROCEDURE VehicleDelete
END
GO

CREATE PROCEDURE VehicleDelete (
	@VehicleID int
) AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM SalesInformation Where VehicleID = @VehicleID;
	DELETE FROM VehicleInventory Where VehicleID = @VehicleID;

	COMMIT TRANSACTION
	END
	GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleSelect')
BEGIN
   DROP PROCEDURE VehicleSelect
END
GO

CREATE PROCEDURE VehicleSelect (
	@VehicleID int
) AS
BEGIN
	SELECT VehicleID, MakeID, ModelID, SalesTypeID, BodyTypeID, YearBuilt, TransmissionID,
	ExtColorID, IntColorID, Mileage, VINNumber, MSRP, SalesPrice, VehicleDescription, IsFeaturedVehicle, ImageFileName

	FROM VehicleInventory
	WHERE VehicleID = @VehicleID

END
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'FeaturedVehicleSelect')
BEGIN
   DROP PROCEDURE FeaturedVehicleSelect
END
GO

CREATE PROCEDURE FeaturedVehicleSelect AS
BEGIN
	SELECT VehicleID, YearBuilt, CarMakes.MakeName, CarModels.ModelName, SalesPrice, ImageFileName
	FROM VehicleInventory
	INNER JOIN CarMakes
			ON VehicleInventory.MakeID = CarMakes.MakeID
		INNER JOIN CarModels
			ON VehicleInventory.ModelID = CarModels.ModelID
	WHERE IsFeaturedVehicle = 1
	ORDER BY MSRP DESC
END
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleSelectDetails')
BEGIN
   DROP PROCEDURE VehicleSelectDetails
END
GO

CREATE PROCEDURE VehicleSelectDetails (
	@VehicleID Int
) AS
BEGIN
	SELECT VehicleID, YearBuilt, CarMakes.MakeName, CarModels.ModelName, ImageFileName, BodyTypes.BodyTypeName, TransmissionTypes.TransmissionTypeName, ExtColors.ExtColorName, IntColors.IntColorName, Mileage, VINNumber, SalesPrice, MSRP, VehicleDescription
	FROM VehicleInventory
	INNER JOIN CarMakes
			ON VehicleInventory.MakeID = CarMakes.MakeID
		INNER JOIN CarModels
			ON VehicleInventory.ModelID = CarModels.ModelID
		INNER JOIN BodyTypes
			ON VehicleInventory.BodyTypeID = BodyTypes.BodyTypeID
		INNER JOIN TransmissionTypes
			ON VehicleInventory.TransmissionID = TransmissionTypes.TransmissionID
		INNER JOIN ExtColors
			ON VehicleInventory.ExtColorID = ExtColors.ExtColorID
		INNER JOIN IntColors
			ON VehicleInventory.IntColorID = IntColors.IntColorID
	WHERE VehicleID = @VehicleID
END

GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleItemsSelectAll')
BEGIN
   DROP PROCEDURE VehicleItemsSelectAll
END
GO

CREATE PROCEDURE VehicleItemsSelectAll AS
BEGIN
	SELECT VehicleID, YearBuilt, CarMakes.MakeName, CarModels.ModelName, ImageFileName, BodyTypes.BodyTypeName, TransmissionTypes.TransmissionTypeName, ExtColors.ExtColorName, IntColors.IntColorName, Mileage, VINNumber, SalesPrice, MSRP
	FROM VehicleInventory
	INNER JOIN CarMakes
			ON VehicleInventory.MakeID = CarMakes.MakeID
		INNER JOIN CarModels
			ON VehicleInventory.ModelID = CarModels.ModelID
		INNER JOIN BodyTypes
			ON VehicleInventory.BodyTypeID = BodyTypes.BodyTypeID
		INNER JOIN TransmissionTypes
			ON VehicleInventory.TransmissionID = TransmissionTypes.TransmissionID
		INNER JOIN ExtColors
			ON VehicleInventory.ExtColorID = ExtColors.ExtColorID
		INNER JOIN IntColors
			ON VehicleInventory.IntColorID = IntColors.IntColorID

		WHERE VehicleID NOT IN
		(SELECT VehicleID 
		from SalesInformation)
END
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialsSelect')

BEGIN
   DROP PROCEDURE SpecialsSelect
END
GO
 
CREATE PROCEDURE SpecialsSelect
AS

SELECT * FROM SalesSpecials
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'MakeInsert')
BEGIN
   DROP PROCEDURE MakeInsert
END
GO
 
CREATE PROCEDURE MakeInsert (
	@MakeID int output,
	@MakeName varchar(50),
	@DateAdded Date,
	@Email nvarchar(256)	
) AS
BEGIN

INSERT INTO CarMakes(MakeName, DateAdded, Email)
VALUES(@MakeName, @DateAdded, @Email)

SET @MakeID = SCOPE_IDENTITY();

END
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ModelInsert')
BEGIN
   DROP PROCEDURE ModelInsert
END
GO
 
CREATE PROCEDURE ModelInsert (
	@ModelID int output,
	@MakeID int,
	@ModelName varchar(50),
	@DateAdded Date,
	@Email nvarchar(256)	
) AS
BEGIN

INSERT INTO CarModels(MakeID, ModelName, DateAdded, Email)
VALUES(@MakeID, @ModelName, @DateAdded, @Email)

SET @ModelID = SCOPE_IDENTITY();

END
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialInsert')
BEGIN
   DROP PROCEDURE SpecialInsert
END
GO
 
CREATE PROCEDURE SpecialInsert (
	@SpecialID int output,
	@SpecialName varchar(200),
	@SpecialDescription varchar(500)
)
AS
BEGIN

INSERT INTO SalesSpecials(SpecialName, SpecialDescription)
VALUES(@SpecialName, @SpecialDescription)

SET @SpecialID = SCOPE_IDENTITY();

END
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactUsInsert')
BEGIN
   DROP PROCEDURE ContactUsInsert
END
GO
 
CREATE PROCEDURE ContactUsInsert (
	@ContactUsID int output,
	@ContactName varchar(100),
	@ContactEmail varchar(255),
	@ContactPhone nvarchar(22),
	@ContactMessage varchar(500)
)
AS
BEGIN

INSERT INTO ContactUsRequest(ContactName, ContactEmail, ContactPhone, ContactMessage)
VALUES(@ContactName, @ContactEmail, @ContactPhone, @ContactMessage)

SET @ContactUsID = SCOPE_IDENTITY();

END
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SalesInsert')
BEGIN
   DROP PROCEDURE SalesInsert
END
GO
 
CREATE PROCEDURE SalesInsert (
	@SalesID int output,
	@CustomerName varchar(100),
	@CustomerPhone nvarchar(22),
	@CustomerEmail nvarchar(255),
	@CustomerStreet1 varchar(200),
	@CustomerStreet2 varchar(200),
	@CustomerCity varchar(100),
	@CustomerState varchar(2),
	@CustomerZip char(5),
	@PurchasePrice decimal(10,2),
	@PurchaseTypeID int,
	@VehicleID int,
	@Id nvarchar(128),
	@PurchaseDate date
)
AS
BEGIN

INSERT INTO SalesInformation(CustomerName, CustomerPhone, CustomerEmail, CustomerStreet1, CustomerStreet2, CustomerCity,
CustomerState, CustomerZip, PurchasePrice, PurchaseTypeID, VehicleID, Id, PurchaseDate)
VALUES(@CustomerName, @CustomerPhone, @CustomerEmail, @CustomerStreet1, @CustomerStreet2, @CustomerCity,
@CustomerState, @CustomerZip, @PurchasePrice, @PurchaseTypeID, @VehicleID, @Id, @PurchaseDate)

SET @SalesID = SCOPE_IDENTITY();

END
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SalesReportSelectAll')
BEGIN
   DROP PROCEDURE SalesReportSelectAll
END
GO

CREATE PROCEDURE SalesReportSelectAll AS
BEGIN
	SELECT CONCAT(AspNetUsers.FirstName, ' ', AspNetUsers.LastName) AS UserName, SUM(SalesInformation.PurchasePrice) AS TotalSales , Count(SalesInformation.SalesID) as TotalVehicles, SalesInformation.PurchaseDate
	FROM AspNetUsers
	JOIN SalesInformation
	ON AspNetUsers.Id = SalesInformation.Id
	GROUP BY AspNetUsers.Id, AspNetUsers.FirstName, AspNetUsers.LastName, SalesInformation.PurchaseDate
	ORDER BY TotalSales Desc
END
GO


IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'NewVehicleInventoryReportSelectAll')
BEGIN
   DROP PROCEDURE NewVehicleInventoryReportSelectAll
END
GO

CREATE PROCEDURE NewVehicleInventoryReportSelectAll AS
BEGIN
	SELECT VehicleInventory.SalesTypeID,VehicleInventory.YearBuilt, CarMakes.MakeName, CarModels.ModelName, COUNT(VehicleInventory.VehicleID) AS [Count], SUM(VehicleInventory.SalesPrice) AS StockValue
	FROM VehicleInventory
	INNER JOIN CarMakes ON
	VehicleInventory.MakeID = CarMakes.MakeID
	INNER JOIN CarModels ON
	VehicleInventory.ModelID = CarModels.ModelID
	WHERE VehicleInventory.SalesTypeID = 1
	AND VehicleID NOT IN
		(SELECT VehicleID 
		from SalesInformation)
	GROUP BY VehicleInventory.SalesTypeID, VehicleInventory.YearBuilt, CarMakes.MakeName, CarModels.ModelName
	ORDER BY CarMakes.MakeName DESC
END
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'UsedVehicleInventoryReportSelectAll')
BEGIN
   DROP PROCEDURE UsedVehicleInventoryReportSelectAll
END
GO

CREATE PROCEDURE UsedVehicleInventoryReportSelectAll AS
BEGIN
	SELECT VehicleInventory.SalesTypeID,VehicleInventory.YearBuilt, CarMakes.MakeName, CarModels.ModelName, COUNT(VehicleInventory.VehicleID) AS [Count], SUM(VehicleInventory.SalesPrice) AS StockValue
	FROM VehicleInventory
	INNER JOIN CarMakes ON
	VehicleInventory.MakeID = CarMakes.MakeID
	INNER JOIN CarModels ON
	VehicleInventory.ModelID = CarModels.ModelID
	WHERE VehicleInventory.SalesTypeID = 2
	AND VehicleID NOT IN
		(SELECT VehicleID 
		from SalesInformation)
	GROUP BY VehicleInventory.SalesTypeID, VehicleInventory.YearBuilt, CarMakes.MakeName, CarModels.ModelName
	ORDER BY CarMakes.MakeName DESC
END
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'NewVehiclesSelectAll')
BEGIN
   DROP PROCEDURE NewVehiclesSelectAll
END
GO

CREATE PROCEDURE NewVehiclesSelectAll AS
BEGIN
	SELECT TOP 20 VehicleID, SalesTypeID, YearBuilt, CarMakes.MakeName, CarModels.ModelName, ImageFileName, BodyTypes.BodyTypeName, TransmissionTypes.TransmissionTypeName, ExtColors.ExtColorName, IntColors.IntColorName, Mileage, VINNumber, SalesPrice, MSRP
	FROM VehicleInventory
	INNER JOIN CarMakes
			ON VehicleInventory.MakeID = CarMakes.MakeID
		INNER JOIN CarModels
			ON VehicleInventory.ModelID = CarModels.ModelID
		INNER JOIN BodyTypes
			ON VehicleInventory.BodyTypeID = BodyTypes.BodyTypeID
		INNER JOIN TransmissionTypes
			ON VehicleInventory.TransmissionID = TransmissionTypes.TransmissionID
		INNER JOIN ExtColors
			ON VehicleInventory.ExtColorID = ExtColors.ExtColorID
		INNER JOIN IntColors
			ON VehicleInventory.IntColorID = IntColors.IntColorID

	WHERE SalesTypeID = 1

	ORDER BY MSRP DESC
	
END
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'UsedVehiclesSelectAll')
BEGIN
   DROP PROCEDURE UsedVehiclesSelectAll
END
GO

CREATE PROCEDURE UsedVehiclesSelectAll AS
BEGIN
	SELECT TOP 20 VehicleID, SalesTypeID, YearBuilt, CarMakes.MakeName, CarModels.ModelName, ImageFileName, BodyTypes.BodyTypeName, TransmissionTypes.TransmissionTypeName, ExtColors.ExtColorName, IntColors.IntColorName, Mileage, VINNumber, SalesPrice, MSRP
	FROM VehicleInventory
	INNER JOIN CarMakes
			ON VehicleInventory.MakeID = CarMakes.MakeID
		INNER JOIN CarModels
			ON VehicleInventory.ModelID = CarModels.ModelID
		INNER JOIN BodyTypes
			ON VehicleInventory.BodyTypeID = BodyTypes.BodyTypeID
		INNER JOIN TransmissionTypes
			ON VehicleInventory.TransmissionID = TransmissionTypes.TransmissionID
		INNER JOIN ExtColors
			ON VehicleInventory.ExtColorID = ExtColors.ExtColorID
		INNER JOIN IntColors
			ON VehicleInventory.IntColorID = IntColors.IntColorID

	WHERE SalesTypeID = 2 
	ORDER BY MSRP DESC
	
END
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialDelete')
BEGIN
   DROP PROCEDURE SpecialDelete
END
GO

CREATE PROCEDURE SpecialDelete (
	@SpecialID int
) AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM SalesSpecials Where SpecialID = @SpecialID;
	
	COMMIT TRANSACTION

	END
	GO

	IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialSelect')
BEGIN
   DROP PROCEDURE SpecialSelect
END
GO

CREATE PROCEDURE SpecialSelect (
	@SpecialID int
) AS
BEGIN
	SELECT SpecialID, SpecialName, SpecialDescription

	FROM SalesSpecials
	WHERE SpecialID = @SpecialID

END
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'UsersSelectAll')

BEGIN
   DROP PROCEDURE UsersSelectAll
END
GO
 
CREATE PROCEDURE UsersSelectAll
AS

SELECT AspNetUsers.Id, FirstName, AspNetUsers.LastName, AspNetUsers.Email, AspNetRoles.Name as [Role]
FROM AspNetUsers
INNER JOIN AspNetUserRoles
ON AspNetUsers.Id = AspNetUserRoles.UserId
INNER JOIN AspNetRoles
ON AspNetUserRoles.RoleId = AspNetRoles.Id
ORDER BY AspNetUsers.LastName DESC

GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'RolesSelectAll')

BEGIN
   DROP PROCEDURE RolesSelectAll
END
GO
 
CREATE PROCEDURE RolesSelectAll
AS

SELECT * FROM AspNetRoles
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SalesInformationSelectAll')

BEGIN
   DROP PROCEDURE SalesInformationSelectAll
END
GO
 
CREATE PROCEDURE SalesInformationSelectAll
AS

SELECT SalesInformation.SalesID, SalesInformation.CustomerName, SalesInformation.CustomerPhone, SalesInformation.CustomerEmail, SalesInformation.CustomerStreet1,
SalesInformation.CustomerStreet2,SalesInformation.CustomerCity, SalesInformation.CustomerState, SalesInformation.CustomerZip, SalesInformation.PurchasePrice,
SalesInformation.PurchaseTypeID, SalesInformation.VehicleID, SalesInformation.PurchaseDate, SalesInformation.Id, CONCAT(AspNetUsers.FirstName, ' ', AspNetUsers.LastName) as UserName
FROM SalesInformation
JOIN AspNetUsers ON
SalesInformation.Id = AspNetUsers.Id
ORDER BY SalesInformation.SalesID
GO