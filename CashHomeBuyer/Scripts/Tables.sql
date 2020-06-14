USE MGAHomes;
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Contact')
DROP TABLE Contact
GO

Create Table Contact (
ContactID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
ContactName VARCHAR(255) NOT NULL,
ContactEmail VARCHAR(255) NOT NULL,
ContactPhone VARCHAR(10) NOT NULL,
ContactDate Date NOT NULL, 
Notes NVARCHAR(MAX) NULL

)

