USE master
GO
 
CREATE LOGIN DvdLibraryApp WITH PASSWORD='testing123'
GO

USE DVDLibrary
GO

CREATE USER DvdLibraryApp FOR LOGIN DvdLibraryApp
GO

GRANT EXECUTE ON GetAllDvds TO DvdLibraryApp
GRANT EXECUTE ON GetDvd TO DvdLibraryApp
GRANT EXECUTE ON AddDvd TO DvdLibraryApp
GRANT EXECUTE ON UpdateDvd TO DvdLibraryApp
GRANT EXECUTE ON DeleteDvd TO DvdLibraryApp

GRANT SELECT ON Dvds to DvdLibraryApp
GRANT INSERT ON Dvds to DvdLibraryApp
GRANT UPDATE ON Dvds to DvdLibraryApp
GRANT DELETE ON Dvds to DvdLibraryApp
GO

