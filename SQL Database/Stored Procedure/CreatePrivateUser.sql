CREATE PROCEDURE CreatePrivateUser @username varchar(300), @password varchar(max), @zipcode int, @CPRNumber int
as
set xact_abort, nocount on;
EXEC ('CreateUser ''' + @username + ''', ''' + @password + ''' ')

INSERT INTO UserTable (Username, CorporateUser, ZipCode, Balance)
VALUES (@username, 0, @zipcode, 0)

INSERT INTO PrivateUser (Username, CPRNumber)
VALUES (@username, @CPRNumber)