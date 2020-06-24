USE MGAHomes;
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactRecordsSelectAll')
BEGIN
   DROP PROCEDURE ContactRecordsSelectAll
END
GO
 
CREATE PROCEDURE ContactRecordsSelectAll
AS

SELECT * FROM Contacts
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactRecordInsert')
BEGIN
   DROP PROCEDURE ContactRecordInsert
END
GO

CREATE PROCEDURE ContactRecordInsert (
	@ContactID int output,
	@ContactAddress varchar(500),
	@ContactName varchar(255),
	@ContactEmail varchar(255),
	@ContactPhone varchar(10),
	@ContactDate Date,
	@Notes nvarchar(max)	
) AS
BEGIN

INSERT INTO Contacts(ContactAddress, ContactName, ContactEmail, ContactPhone, ContactDate, Notes)
VALUES(@ContactAddress, @ContactName, @ContactEmail, @ContactPhone, @ContactDate, @Notes)

SET @ContactID = SCOPE_IDENTITY();

END
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactUpdate')
BEGIN
   DROP PROCEDURE ContactUpdate
END
GO
 
CREATE PROCEDURE ContactUpdate (
	@ContactID int,
	@ContactAddress varchar(500),
	@ContactName varchar(255),
	@ContactEmail varchar(255),
	@ContactPhone varchar(10),
	@ContactDate date,
	@Notes nvarchar(max))

AS
BEGIN

UPDATE Contacts SET
    ContactAddress = @ContactAddress,
	ContactName = @ContactName,
	ContactEmail = @ContactEmail,
	ContactPhone = @ContactPhone,
	ContactDate = @ContactDate,
	Notes = @Notes
	
WHERE ContactID = @ContactID

END 
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactSelect')
BEGIN
   DROP PROCEDURE ContactSelect
END
GO

CREATE PROCEDURE ContactSelect (
	@ContactID int
) AS
BEGIN
	SELECT ContactID, ContactAddress, ContactName, ContactEmail, ContactPhone, ContactDate, Notes

	FROM Contacts
	WHERE ContactID = @ContactID

END
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'AddNotes')
BEGIN
   DROP PROCEDURE AddNotes
END
GO

CREATE PROCEDURE AddNotes (
	@ContactID int,
	@Notes varchar(max)
) AS
BEGIN

UPDATE Contacts SET
Notes = @Notes

WHERE ContactID = @ContactID

END 
GO