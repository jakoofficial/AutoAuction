CREATE PROCEDURE CreateCorporateUser @username varchar(300), @password varchar(max), @zipcode int, @CVRNumber int
as
set xact_abort, nocount on;

IF EXISTS(SELECT Username FROM UserTable WHERE Username = @username)
BEGIN 
	;THROW 99001, 'The username is already taken', 1;
END

IF EXISTS(SELECT CVRNumber FROM CorporateUser WHERE CVRNumber = @CVRNumber)
BEGIN 
	;THROW 99002, 'The CVR number is already in use', 1;
END

EXEC ('CreateLogin ''' + @username + ''', ''' + @password + ''' ')

INSERT INTO UserTable (Username, CorporateUser, ZipCode, Balance, Credit)
VALUES (@username, 1, @zipcode, 0, 0)

INSERT INTO CorporateUser (Username, CVRNumber)
VALUES (@username, @CVRNumber)