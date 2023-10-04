CREATE PROCEDURE CreatePrivateUser @username varchar(300), @password varchar(max), @zipcode int, @CPRNumber bigint
as
set xact_abort, nocount on;

IF EXISTS(SELECT Username FROM UserTable WHERE Username = @username)
BEGIN 
	;THROW 99001, 'The username is already taken', 1;
END

IF EXISTS(SELECT CPRNumber FROM PrivateUser WHERE CPRNumber = @CPRNumber)
BEGIN 
	;THROW 99002, 'The CPR number is already in use', 1;
END

EXEC ('CreateLogin ''' + @username + ''', ''' + @password + ''' ')

INSERT INTO UserTable (Username, CorporateUser, ZipCode, Balance)
VALUES (@username, 0, @zipcode, 0)

INSERT INTO PrivateUser (Username, CPRNumber)
VALUES (@username, @CPRNumber)