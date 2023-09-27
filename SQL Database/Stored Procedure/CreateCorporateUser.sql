CREATE PROCEDURE CreateCorporateUser @username varchar(300), @password varchar(max), @CVRNumber int
as
set xact_abort, nocount on;

EXEC ('CreateUser ''' + @username + ''', ''' + @password + ''' ')

INSERT INTO UserTable (Username, CorporateUser, Balance, Credit)
VALUES (@username, 1, 0, 0)

INSERT INTO CorporateUser (Username, CVRNumber)
VALUES (@username, @CVRNumber)