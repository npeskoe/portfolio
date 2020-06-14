USE MGAHomes
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DbReset')
   DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset AS
   BEGIN
	DELETE FROM Contacts;

	DBCC CHECKIDENT ('Contacts', RESEED, 1)

	SET IDENTITY_INSERT Contacts ON;

	INSERT INTO Contacts(ContactID, ContactAddress, ContactName, ContactEmail, ContactPhone, ContactDate, Notes)
	VALUES(1, '123 E Main St Louisville, KY 40203', 'Bob Smith', 'bob.smith@gmail.com', 5021234567, '06/03/2020', 'This is a good lead'),
	(2, '200 Idlewylde Dr Louisville, KY 40206','Mike Davis', 'mike.davis@gmail.com', 5029876543, '06/03/2020', null),
	(3, '1404 E Jackson St Louisville, KY 40217', 'Gene Jacobs', 'gene.jacobs@gmail.com', 5024566543, '06/03/2020', null)

	SET IDENTITY_INSERT Contacts OFF;

	END