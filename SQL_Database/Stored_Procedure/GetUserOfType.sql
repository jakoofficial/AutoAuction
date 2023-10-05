drop procedure if exists GetUserOfType
GO

CREATE PROCEDURE GetUserOfType @username varchar(300)
AS
declare @usertype bit;

SELECT @usertype = dbo.UserTable.CorporateUser FROM dbo.UserTable WHERE UserTable.Username = @username

IF @usertype = 1 BEGIN
	SELECT UserTable.Username, UserTable.CorporateUser, UserTable.ZipCode, UserTable.Credit, UserTable.Balance, CorporateUser.CVRNumber FROM UserTable 
	INNER JOIN dbo.CorporateUser ON dbo.CorporateUser.Username = UserTable.Username
	WHERE UserTable.Username = @username
END
ELSE
BEGIN
	SELECT UserTable.Username, UserTable.CorporateUser, UserTable.ZipCode, UserTable.Balance, PrivateUser.CPRNumber FROM UserTable 
	INNER JOIN dbo.PrivateUser ON dbo.PrivateUser.Username = UserTable.Username
	WHERE UserTable.Username = @username
END