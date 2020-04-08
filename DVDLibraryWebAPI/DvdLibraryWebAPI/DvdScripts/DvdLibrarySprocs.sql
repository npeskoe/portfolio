USE DVDLibrary
GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'GetAllDvds')

BEGIN
   DROP PROCEDURE GetAllDvds
END
GO
 
CREATE PROCEDURE GetAllDvds
AS

SELECT d.DvdID, d.Title, d.ReleaseYear, d.Director, d.Rating, d.Notes
FROM Dvds d

GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'GetDvd')

BEGIN
   DROP PROCEDURE GetDvd
END
GO
 
CREATE PROCEDURE GetDvd
AS

SELECT d.DvdID, d.Title, d.ReleaseYear, d.Director, d.Rating, d.Notes
FROM Dvds d 
WHERE d.DvdID = dvdID

GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'AddDvd')

BEGIN
   DROP PROCEDURE AddDvd
END
GO
 
CREATE PROCEDURE AddDvd(@Title varchar(50), @ReleaseYear int, @Director varchar(50),@Rating varchar(5),
@Notes varchar(500))
AS

INSERT INTO [dbo].Dvds([Title], [ReleaseYear], [Director], [Rating], [Notes])
VALUES (@Title, @ReleaseYear, @Director, @Rating, @Notes)

GO


IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'UpdateDvd')

BEGIN
   DROP PROCEDURE UpdateDvd
END
GO
 
CREATE PROCEDURE UpdateDvd(@Title varchar(50), @ReleaseYear int, @Director varchar(50),@Rating varchar(5),
@Notes varchar(500), @DvdID int)
AS

UPDATE dbo.Dvds          

SET Title = @Title,
ReleaseYear = @ReleaseYear,
Director = @Director,
Rating = @Rating,
Notes = @Notes
WHERE DvdID = @DvdID;

GO

IF EXISTS(
   SELECT *
   FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DeleteDvd')

BEGIN
   DROP PROCEDURE DeleteDvd
END
GO
 
CREATE PROCEDURE DeleteDvd(@DvdID int)
AS

DELETE FROM dbo.Dvds          
WHERE DvdID = @DvdID;

GO




