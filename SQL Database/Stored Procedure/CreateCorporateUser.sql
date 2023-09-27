CREATE PROCEDURE CreateCorporateUser @username varchar(300), @password varchar(max), @zipcode int, @CVRNumber int
as
set xact_abort, nocount on;

EXEC ('CreateUser ''' + @username + ''', ''' + @password + ''' ')

INSERT INTO UserTable (Username, CorporateUser, ZipCode, Balance, Credit)
VALUES (@username, 1, @zipcode, 0, 0)

INSERT INTO CorporateUser (Username, CVRNumber)
VALUES (@username, @CVRNumber)