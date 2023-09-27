CREATE PROCEDURE CreatePrivateUser @username varchar(300), @password varchar(max), @CPRNumber int
as
EXEC ('CreateUser ''' + @username + ''', ''' + @password + ''' ')

INSERT INTO UserTable (Username, CorporateUser, Balance)
VALUES (@username, 0, 0)

INSERT INTO PrivateUser (Username, CPRNumber)
VALUES (@username, @CPRNumber)